using DGP.Snap.Service.Kernel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;

namespace DGP.Snap.Service.Shell
{
    public class TaskBarService : IDisposable
    {
        //获取当前桌面的根
        private static readonly AutomationElement Desktop = AutomationElement.RootElement;

        private static AutomationEventHandler automationEventHandler;
        

        private readonly int currentRefreshRate = Device.Monitor.CurrentRefreshRate();

        #region const value
        private const int OneSecond = 1000;
        private const string MSTaskListWClass = "MSTaskListWClass";
        private const string Shell_TrayWnd = "Shell_TrayWnd";
        private const string Shell_SecondaryTrayWnd = "Shell_SecondaryTrayWnd";
        #endregion

        private readonly List<AutomationElement> _bars = new List<AutomationElement>();
        private readonly Dictionary<AutomationElement, AutomationElement> _children = new Dictionary<AutomationElement, AutomationElement>();
        private readonly Dictionary<AutomationElement, double> _lasts = new Dictionary<AutomationElement, double>();
        private readonly Dictionary<AutomationElement, Task> repositionThreads = new Dictionary<AutomationElement, Task>();

        private CancellationTokenSource loopCancellationTokenSource = new CancellationTokenSource();

        private static AutomationPropertyChangedEventHandler propertyChangedHandler;

        public bool Applied = false;

        public void Centerlize()
        {
            Applied = true;
            PropertyCondition isShell_TrayWnd = new PropertyCondition(AutomationElement.ClassNameProperty, Shell_TrayWnd);
            PropertyCondition isShell_SecondaryTrayWnd = new PropertyCondition(AutomationElement.ClassNameProperty, Shell_SecondaryTrayWnd);
            OrCondition condition = new OrCondition(isShell_TrayWnd, isShell_SecondaryTrayWnd);

            CacheRequest cacheRequest = new CacheRequest();
            cacheRequest.Add(AutomationElement.NameProperty);
            cacheRequest.Add(AutomationElement.BoundingRectangleProperty);

            _bars.Clear();
            _children.Clear();
            _lasts.Clear();

            using (cacheRequest.Activate())
            {
                AutomationElementCollection elements = Desktop.FindAll(TreeScope.Children, condition);
                if (elements == null)
                    return;
                _lasts.Clear();

                Parallel.ForEach(elements.OfType<AutomationElement>(), trayWnd =>
                {
                    //find taskbar
                    AutomationElement taskbar = trayWnd.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.ClassNameProperty, MSTaskListWClass));
                    if (taskbar != null)
                    {
                        propertyChangedHandler = OnUIAutomationEvent;
                        Automation.AddAutomationPropertyChangedEventHandler(taskbar, TreeScope.Element, propertyChangedHandler, AutomationElement.BoundingRectangleProperty);

                        _bars.Add(trayWnd);
                        _children.Add(trayWnd, taskbar);

                        repositionThreads[trayWnd] = Task.Run(() => LoopForReposition(trayWnd), loopCancellationTokenSource.Token);
                    }
                });
            }

            automationEventHandler = OnUIAutomationEvent;
            Automation.AddAutomationEventHandler(WindowPattern.WindowOpenedEvent, Desktop, TreeScope.Subtree, automationEventHandler);
            Automation.AddAutomationEventHandler(WindowPattern.WindowClosedEvent, Desktop, TreeScope.Subtree, automationEventHandler);

            SystemEvents.DisplaySettingsChanging += SystemEvents_DisplaySettingsChanged;
        }

        private void OnUIAutomationEvent(object src, AutomationEventArgs e)
        {
            //restrat loop task
            Parallel.ForEach(_bars.ToList(), trayWnd =>
            {
                if (repositionThreads[trayWnd].IsCompleted)
                    repositionThreads[trayWnd] = Task.Run(() => LoopForReposition(trayWnd), loopCancellationTokenSource.Token);
            });
        }

        private void LoopForReposition(object trayWndObj)
        {
            AutomationElement trayWnd = (AutomationElement)trayWndObj;
            int numberOfLoops = currentRefreshRate / 10;
            int keepGoing = 0;
            while (keepGoing < numberOfLoops)
            {
                if (!PositionLoop(trayWnd)) 
                    keepGoing += 1;
                if (loopCancellationTokenSource.IsCancellationRequested)
                    break;
                Task.Delay(OneSecond / currentRefreshRate).Wait();
            }
        }

        private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e) => Restart(sender, e);

        private bool PositionLoop(AutomationElement trayWnd)
        {
            AutomationElement taskList = _children[trayWnd];
            AutomationElement last = TreeWalker.ControlViewWalker.GetLastChild(taskList);
            if (last == null)
                return true;

            Rect trayBounds = trayWnd.Cached.BoundingRectangle;
            bool horizontal = trayBounds.Width > trayBounds.Height;

            // Use the left/top bounds because there is an empty element as the last child with a nonzero width
            var lastChildPos = horizontal ? last.Current.BoundingRectangle.Left : last.Current.BoundingRectangle.Top;

            if (_lasts.ContainsKey(trayWnd) && lastChildPos == _lasts[trayWnd])
                return false;

            _lasts[trayWnd] = lastChildPos;

            if (TreeWalker.ControlViewWalker.GetFirstChild(taskList) == null)
                return true;

            double scale = horizontal
                ? last.Current.BoundingRectangle.Height / trayBounds.Height
                : last.Current.BoundingRectangle.Width / trayBounds.Width;

            double size = (lastChildPos - (horizontal
                            ? TreeWalker.ControlViewWalker.GetFirstChild(taskList).Current.BoundingRectangle.Left
                            : TreeWalker.ControlViewWalker.GetFirstChild(taskList).Current.BoundingRectangle.Top)
                        ) / scale;
            if (size < 0)
                return true;

            if (TreeWalker.ControlViewWalker.GetParent(taskList) == null)
                return true;


            Rect taskListBounds = taskList.Current.BoundingRectangle;

            double barSize = horizontal ? trayWnd.Cached.BoundingRectangle.Width : trayWnd.Cached.BoundingRectangle.Height;
            double targetPos = Math.Round((barSize - size) / 2) + (horizontal ? trayBounds.X : trayBounds.Y);

            double delta = Math.Abs(targetPos - (horizontal ? taskListBounds.X : taskListBounds.Y));
            // Previous bounds check
            if (delta <= 1)
                return false;// Already positioned within margin of error, avoid the unneeded MoveWindow call

            // Right bounds check
            int rightBounds = SideBoundary(false, horizontal, taskList);
            if (targetPos + size > rightBounds)
            {
                // Shift off center when the bar is too big
                double extra = targetPos + size - rightBounds;
                targetPos -= extra;
            }

            // Left bounds check
            int leftBounds = SideBoundary(true, horizontal, taskList);
            if (targetPos <= leftBounds)
            {
                // Prevent X position ending up beyond the normal left aligned position
                Reset(trayWnd);
                return true;
            }

            IntPtr taskListPtr = (IntPtr)taskList.Current.NativeWindowHandle;

            if (horizontal)
                NativeMethod.SetWindowPos(taskListPtr, IntPtr.Zero, RelativePos(targetPos, horizontal, taskList), 0, 0, 0,
                    NativeMethod.SWP_NOZORDER | NativeMethod.SWP_NOSIZE | NativeMethod.SWP_ASYNCWINDOWPOS);
            else
                NativeMethod.SetWindowPos(taskListPtr, IntPtr.Zero, 0, RelativePos(targetPos, horizontal, taskList), 0, 0,
                    NativeMethod.SWP_NOZORDER | NativeMethod.SWP_NOSIZE | NativeMethod.SWP_ASYNCWINDOWPOS);

            _lasts[trayWnd] = horizontal ? last.Current.BoundingRectangle.Left : last.Current.BoundingRectangle.Top;

            return true;
        }

        private void Restart(object sender, EventArgs e)
        {
            CancelPositionThread();
            Centerlize();
        }

        private static int SideBoundary(bool left, bool horizontal, AutomationElement element)
        {
            double adjustment = 0;
            AutomationElement prevSibling = TreeWalker.ControlViewWalker.GetPreviousSibling(element);
            AutomationElement nextSibling = TreeWalker.ControlViewWalker.GetNextSibling(element);
            AutomationElement parent = TreeWalker.ControlViewWalker.GetParent(element);
            if (left && prevSibling != null && !prevSibling.Current.BoundingRectangle.IsEmpty)
            {
                adjustment = horizontal
                    ? prevSibling.Current.BoundingRectangle.Right
                    : prevSibling.Current.BoundingRectangle.Bottom;
            }
            else if (!left && nextSibling != null && !nextSibling.Current.BoundingRectangle.IsEmpty)
            {
                adjustment = horizontal
                    ? nextSibling.Current.BoundingRectangle.Left
                    : nextSibling.Current.BoundingRectangle.Top;
            }
            else if (parent != null)
            {
                if (horizontal)
                    adjustment = left ? parent.Current.BoundingRectangle.Left : parent.Current.BoundingRectangle.Right;
                else
                    adjustment = left ? parent.Current.BoundingRectangle.Top : parent.Current.BoundingRectangle.Bottom;
            }

            //if (horizontal)
            //    Debug.WriteLine((left ? "Left" : "Right") + " side boundary calculated at " + adjustment);
            //else
            //    Debug.WriteLine((left ? "Top" : "Bottom") + " side boundary calculated at " + adjustment);

            return (int)adjustment;
        }

        private static void Reset(AutomationElement trayWnd)
        {
            AutomationElement taskList = trayWnd.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.ClassNameProperty, MSTaskListWClass));
            if (taskList == null)
                return;

            if (TreeWalker.ControlViewWalker.GetParent(taskList) == null)
                return;

            IntPtr taskListPtr = (IntPtr)taskList.Current.NativeWindowHandle;
            NativeMethod.SetWindowPos(taskListPtr, IntPtr.Zero, 0, 0, 0, 0, NativeMethod.SWP_NOZORDER | NativeMethod.SWP_NOSIZE | NativeMethod.SWP_ASYNCWINDOWPOS);
        }

        private static int RelativePos(double x, bool horizontal, AutomationElement element)
        {
            int adjustment = SideBoundary(true, horizontal, element);
            double newPos = x - adjustment;

            if (newPos < 0)
                newPos = 0;

            return (int)newPos;
        }

        private void CancelPositionThread()
        {
            loopCancellationTokenSource.Cancel();
            Parallel.ForEach(repositionThreads.Values.ToList(), theTask =>
            {
                try
                {
                    // Give the thread time to exit gracefully.
                    if (theTask.Wait(OneSecond * 3)) return;
                }
                catch (OperationCanceledException e)
                {
                    Console.WriteLine($"{nameof(OperationCanceledException)} thrown with message: {e.Message}");
                }
                finally
                {
                    theTask.Dispose();
                }
            });

            loopCancellationTokenSource = new CancellationTokenSource();
        }

        private void ResetAll()
        {
            CancelPositionThread();
            Parallel.ForEach(_bars.ToList(), Reset);
        }

        public void ReturnNormal()
        {
            if (automationEventHandler != null)
            {
                foreach (var taskBar in _children)
                {
                    Automation.RemoveAutomationPropertyChangedEventHandler(taskBar.Value, propertyChangedHandler);
                }

                Automation.RemoveAutomationEventHandler(WindowPattern.WindowOpenedEvent, Desktop, automationEventHandler);
                Automation.RemoveAutomationEventHandler(WindowPattern.WindowClosedEvent, Desktop, automationEventHandler);
            }

            // Put icons back
            ResetAll();
            Applied = false;
        }

        private static bool _disposed;
        private static bool disposing;
        public void Dispose()
        {
            if (_disposed)
                return;
            // Stop listening for new events
            if (automationEventHandler != null)
            {
                foreach (var taskBar in _children)
                {
                    Automation.RemoveAutomationPropertyChangedEventHandler(taskBar.Value, propertyChangedHandler);
                }

                Automation.RemoveAutomationEventHandler(WindowPattern.WindowOpenedEvent, Desktop, automationEventHandler);
                Automation.RemoveAutomationEventHandler(WindowPattern.WindowClosedEvent, Desktop, automationEventHandler);
            }

            // Put icons back
            ResetAll();

            Applied = false;
            _disposed = true;
        }

        #region 单例
        private static TaskBarService instance;
        private static object _lock = new object();
        public static TaskBarService GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new TaskBarService();
                    }
                }
            }
            return instance;
        }
        #endregion
    }
}
