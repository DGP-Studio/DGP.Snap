using DGP.Snap.Service.Kernel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace DGP.Snap.Helper.Extensions
{
    public static class WindowExtension
    {
        public static IntPtr GetHandle(this System.Windows.Window window)
        {
            return new WindowInteropHelper(window).Handle;
        }

        public static void SetBottomMost(this System.Windows.Window window)
        {
            IntPtr hWndWindow = new WindowInteropHelper(window).Handle;

            //NativeMethod.SetWindowPos(hWndWindow, NativeMethod.HWND_BOTTOM, 0, 0, 0, 0,
            //    NativeMethod.SWP_NOSIZE | NativeMethod.SWP_NOMOVE | NativeMethod.SWP_NOACTIVATE/* | NativeMethod.SWP_SHOWWINDOW*/);

            //IntPtr hWndProgMan = NativeMethod.FindWindow("Progman", null/*"Program Manager"*/);
            //IntPtr hWndShellDllDefView = NativeMethod.FindWindowEx(hWndProgMan, IntPtr.Zero, "SHELLDLL_DefView", "");          
            //IntPtr hWndSysListView32 = NativeMethod.FindWindowEx(hWndShellDllDefView, IntPtr.Zero, "SysListView32", "FolderView");
            //NativeMethod.SetParent(hWndWindow, hWndProgMan);
            //NativeMethod.SetWindowLong(hWndWindow, NativeMethod.GWL_HWNDPARENT, hWndSysListView32);

            IntPtr hWndProgMan = NativeMethod.FindWindow("Progman", null/*"Program Manager"*/);
            Debug.WriteLine("Program Manager"+hWndProgMan.ToString());
            NativeMethod.SendMessageTimeout(hWndProgMan, 1324u, new UIntPtr(0u), IntPtr.Zero, NativeMethod.SendMessageTimeoutFlags.SMTO_NORMAL, 1000u, out UIntPtr _);
            NativeMethod.SetParent(hWndWindow, FindWorkerWPtr());


        }

        private static IntPtr FindWorkerWPtr()
        {
            IntPtr workerw = IntPtr.Zero;
            IntPtr def = IntPtr.Zero;
            IntPtr hWndProgMan = NativeMethod.FindWindow("Progman", null);

            NativeMethod.EnumWindows(
                (IntPtr handle, IntPtr param)=>
                {
                    if ((def = NativeMethod.FindWindowEx(handle, IntPtr.Zero, "SHELLDLL_DefView", IntPtr.Zero)) != IntPtr.Zero)
                    {
                        workerw = NativeMethod.FindWindowEx(IntPtr.Zero, handle, "WorkerW", IntPtr.Zero);
                        Console.Write("workerw:" + workerw.ToString() + "\n");
                        NativeMethod.ShowWindow(workerw, 0);
                    }
                    return true;
            }, IntPtr.Zero);
            return hWndProgMan;
        }
    }
}
