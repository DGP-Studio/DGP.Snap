using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Interop;

using static DGP.Snap.Service.Kernel.NativeMethod;

namespace DGP.Snap.Helper.Extensions
{
    public static class WindowExtensions
    {
        /// <summary>
        /// 获取窗体的句柄
        /// </summary>
        /// <param name="window"></param>
        /// <returns>整型窗体句柄指针</returns>
        public static IntPtr GetHandle(this System.Windows.Window window)
        {
            return new WindowInteropHelper(window).Handle;
        }

        /// <summary>
        /// 将窗体置底,不能交互，用于动态壁纸
        /// </summary>
        /// <param name="window"></param>
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

        /// <summary>
        /// 查找Windows的WorkerW
        /// </summary>
        /// <returns>整型窗体句柄指针</returns>
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

        /// <summary>
        /// 将窗体置底同时保留交互
        /// </summary>
        /// <param name="window"></param>
        public static void SetBottomWithInteractivity(this System.Windows.Window window)
        {
            IntPtr hWndWindow = new WindowInteropHelper(window).Handle;

            SetWindowPos(hWndWindow, HWND_BOTTOM, 0, 0, 0, 0,
                SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE | SWP_SHOWWINDOW);
        }

        /// <summary>
        /// 为窗体设置Win10亚克力背景
        /// </summary>
        /// <param name="window"></param>
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
