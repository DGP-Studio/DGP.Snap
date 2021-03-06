﻿using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

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
#pragma warning disable AV1706 // Identifier contains an abbreviation or is too short
        [DllImport("user32.dll")] public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
#pragma warning restore AV1706 // Identifier contains an abbreviation or is too short

        public const uint SWP_NOSIZE = 0x0001;
        public const uint SWP_NOMOVE = 0x0002;
        public const uint SWP_NOZORDER = 0x0004;
        public const uint SWP_NOACTIVATE = 0x0010;
        public const uint SWP_SHOWWINDOW = 0x0040;
        public const int SWP_ASYNCWINDOWPOS = 0x4000;


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
#pragma warning disable AV1706 // Identifier contains an abbreviation or is too short
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)] public static extern IntPtr FindWindowEx(IntPtr hWndParent, IntPtr hWndChildAfter, string lpszClass, IntPtr windowTitle);
#pragma warning restore AV1706 // Identifier contains an abbreviation or is too short
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

#pragma warning disable AV1562 // Do not declare a parameter as ref or out
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)] public static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, UIntPtr wParam, IntPtr lParam, SendMessageTimeoutFlags fuFlags, uint uTimeout, out UIntPtr lpdwResult);
#pragma warning restore AV1562 // Do not declare a parameter as ref or out
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
#pragma warning disable AV1562 // Do not declare a parameter as ref or out
        [DllImport("user32.dll")] public static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
#pragma warning restore AV1562 // Do not declare a parameter as ref or out
        #endregion

        #region EnumDisplaySettings
        public const int ENUM_CURRENT_SETTINGS = -1;
        public const int ENUM_REGISTRY_SETTINGS = -2;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DEVMODE
        {
            private const int CCHDEVICENAME = 32;
            private const int CCHFORMNAME = 32;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public ScreenOrientation dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }

        [DllImport("user32.dll")] public static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);

        #endregion

        #endregion

        #region Kernel32

        #region GlobalMemoryStatus
        [StructLayout(LayoutKind.Sequential)]
#pragma warning disable AV1706 // Identifier contains an abbreviation or is too short
        public struct MemoryStatusEx
#pragma warning restore AV1706 // Identifier contains an abbreviation or is too short
        {
            public uint dwLength; //当前结构体大小
            public uint dwMemoryLoad; //当前内存使用率
            public ulong ullTotalPhys; //总计物理内存大小
            public ulong ullAvailPhys; //可用物理内存大小
            public ulong ullTotalPageFile; //总计交换文件大小
            public ulong ullAvailPageFile; //总计交换文件大小
            public ulong ullTotalVirtual; //总计虚拟内存大小
            public ulong ullAvailVirtual; //可用虚拟内存大小
            public ulong ullAvailExtendedVirtual; //保留 这个值始终为0
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
#pragma warning disable AV1706 // Identifier contains an abbreviation or is too short
#pragma warning disable AV1562 // Do not declare a parameter as ref or out
        public static extern bool GlobalMemoryStatusEx(ref MemoryStatusEx meminfo);
#pragma warning restore AV1562 // Do not declare a parameter as ref or out
#pragma warning restore AV1706 // Identifier contains an abbreviation or is too short

        #endregion

        #endregion
    }
}

