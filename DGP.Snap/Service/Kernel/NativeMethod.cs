using System;
using System.Runtime.InteropServices;

namespace DGP.Snap.Service.Kernel
{
    public class NativeMethod
    {
        #region User32

        #region SetWindowPos
        /// <summary>
        /// 更改一个子窗口，弹出窗口或顶级窗口的大小，位置和Z顺序。
        /// 这些窗口根据其在屏幕上的显示方式进行排序。
        /// 最顶层的窗口有最高排序，是Z顺序中的第一个窗口
        /// </summary>
        /// <param name="hWnd"><see cref="System.Windows.Window"/>实例的句柄</param>
        /// <param name="hWndInsertAfter">窗口的句柄，位于Z顺序中定位窗口之前</param>
        /// <param name="X">按客户端坐标，相对窗口左侧的新位置</param>
        /// <param name="Y">按客户端坐标，相对窗口上侧的新位置</param>
        /// <param name="cx">窗口的新宽度（以像素为单位）</param>
        /// <param name="cy">窗口的新高度（以像素为单位）</param>
        /// <param name="uFlags">窗口大小和定位标志</param>
        /// <returns>如果函数调用成功，则返回值为<see cref="true"/></returns>
        [DllImport("user32.dll")] public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public const uint SWP_NOSIZE = 0x0001;
        public const uint SWP_NOMOVE = 0x0002;
        public const uint SWP_NOACTIVATE = 0x0010;
        public const uint SWP_SHOWWINDOW = 0x0040;
        public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        #endregion

        #region SetWindowLong
        /// <summary>
        /// 更改指定窗口的属性。函数还在窗口额外内存中的指定偏移处设置一个值。
        /// </summary>
        /// <param name="hWnd">窗口的句柄,间接指向窗口所属的类</param>
        /// <param name="nIndex">对要设置的值的零基偏移量</param>
        /// <param name="dwNewLong">替换值</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)] public static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        /// <summary>
        /// 这个有点问题，官方文档没有记录
        /// </summary>
        public const int GWL_HWNDPARENT = -8;
        #endregion

        #region FindWindow
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lpWindowClass"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)] public static extern IntPtr FindWindow(string lpWindowClass, string lpWindowName);
        #endregion

        #region FindWindwEx
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentHandle"></param>
        /// <param name="childAfter"></param>
        /// <param name="className"></param>
        /// <param name="windowTitle"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)] public static extern IntPtr FindWindowEx(IntPtr hWndParent, IntPtr hWndChildAfter, string lpszClass, IntPtr windowTitle);
        #endregion

        #region SetParent
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWndChild"></param>
        /// <param name="hWndNewParent"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)] public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        #endregion

        #region SendMessageTimeout

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)] public static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, UIntPtr wParam, IntPtr lParam, SendMessageTimeoutFlags fuFlags, uint uTimeout, out UIntPtr lpdwResult);
        public enum SendMessageTimeoutFlags : uint
        {
            SMTO_NORMAL = 0x0,
            SMTO_BLOCK = 0x1,
            SMTO_ABORTIFHUNG = 0x2,
            SMTO_NOTIMEOUTIFNOTHUNG = 0x8,
            SMTO_ERRORONEXIT = 0x20
        }

        #endregion

        #region EnumWindows
        [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);
        #endregion

        #region ShowWindow
        [DllImport("user32.dll", CharSet = CharSet.Auto)] public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
        #endregion

        #region SetWindowCompositionAttribute
        public enum AccentState
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_INVALID_STATE = 4
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }
        public enum WindowCompositionAttribute
        {
            WCA_UNDEFINED = 0,
            WCA_NCRENDERING_ENABLED = 1,
            WCA_NCRENDERING_POLICY = 2,
            WCA_TRANSITIONS_FORCEDISABLED = 3,
            WCA_ALLOW_NCPAINT = 4,
            WCA_CAPTION_BUTTON_BOUNDS = 5,
            WCA_NONCLIENT_RTL_LAYOUT = 6,
            WCA_FORCE_ICONIC_REPRESENTATION = 7,
            WCA_EXTENDED_FRAME_BOUNDS = 8,
            WCA_HAS_ICONIC_BITMAP = 9,
            WCA_THEME_ATTRIBUTES = 10,
            WCA_NCRENDERING_EXILED = 11,
            WCA_NCADORNMENTINFO = 12,
            WCA_EXCLUDED_FROM_LIVEPREVIEW = 13,
            WCA_VIDEO_OVERLAY_ACTIVE = 14,
            WCA_FORCE_ACTIVEWINDOW_APPEARANCE = 15,
            WCA_DISALLOW_PEEK = 16,
            WCA_CLOAK = 17,
            WCA_CLOAKED = 18,
            WCA_ACCENT_POLICY = 19,
            WCA_FREEZE_REPRESENTATION = 20,
            WCA_EVER_UNCLOAKED = 21,
            WCA_VISUAL_OWNER = 22,
            WCA_LAST = 23,
        }
        [DllImport("user32.dll")] public static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
        #endregion

        #endregion

        #region Kernel32

        #region GlobalMemoryStatus
        [StructLayout(LayoutKind.Sequential)]
        public struct MemoryInfo
        {
            public uint Length;
            public uint MemoryLoad;
            public uint TotalPhysical;//总内存
            public uint AvailablePhysical;//可用物理内存
            public uint TotalPageFile;
            public uint AvailablePageFile;
            public uint TotalVirtual;
            public uint AvailableVirtual;
        }

        [DllImport("kernel32")]
        public static extern void GlobalMemoryStatus(ref MemoryInfo meminfo);
        #endregion

        #endregion
    }
}

