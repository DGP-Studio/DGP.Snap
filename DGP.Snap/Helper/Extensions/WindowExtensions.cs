using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Interop;

using static DGP.Snap.Service.Kernel.NativeMethod;

namespace DGP.Snap.Helper.Extensions
{
    public static class WindowExtensions
    {
        public static IntPtr GetHandle(this System.Windows.Window window)
        {
            return new WindowInteropHelper(window).Handle;
        }
        public static void SetBottomMost(this System.Windows.Window window)
        {
            IntPtr hWndWindow = new WindowInteropHelper(window).Handle;

            IntPtr hWndProgMan = FindWindow("Progman", null/*"Program Manager"*/);
            Debug.WriteLine("Program Manager" + hWndProgMan.ToString());
            //下面似乎是用来创建WorkerW的
            SendMessageTimeout(hWndProgMan,
                                            0x052C,
                                            new UIntPtr(0u),
                                            IntPtr.Zero,
                                            SendMessageTimeoutFlags.SMTO_NORMAL,
                                            1000u,
                                            out UIntPtr _);
            SetParent(hWndWindow, FindWorkerWPtr());
        }
        private static IntPtr FindWorkerWPtr()
        {
            IntPtr workerw = IntPtr.Zero;
            IntPtr def = IntPtr.Zero;
            IntPtr hWndProgMan = FindWindow("Progman", null);

            EnumWindows(
                (IntPtr handle, IntPtr param) =>
                {
                    if ((def = FindWindowEx(handle, IntPtr.Zero, "SHELLDLL_DefView", IntPtr.Zero)) != IntPtr.Zero)
                    {
                        workerw = FindWindowEx(IntPtr.Zero, handle, "WorkerW", IntPtr.Zero);
                        Console.Write("workerw:" + workerw.ToString() + "\n");
                        ShowWindow(workerw, 0);
                    }
                    return true;
                }, IntPtr.Zero);
            return hWndProgMan;
        }
        public static void SetBottomWithInteractivity(this System.Windows.Window window)
        {
            IntPtr hWndWindow = new WindowInteropHelper(window).Handle;

            SetWindowPos(hWndWindow, HWND_BOTTOM, 0, 0, 0, 0,
                SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE | SWP_SHOWWINDOW);
        }

        public static void SetAcrylicblur(this System.Windows.Window window)
        {

            AccentPolicy accent = new AccentPolicy
            {
                AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND
            };

            int accentStructSize = Marshal.SizeOf(accent);

            IntPtr accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            WindowCompositionAttributeData data = new WindowCompositionAttributeData
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };

            SetWindowCompositionAttribute(window.GetHandle(), ref data);

            Marshal.FreeHGlobal(accentPtr);
        }
    }
}
