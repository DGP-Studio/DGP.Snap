using DGP.Snap.Window.FrontSight;
using DGP.Snap.Window.Wallpaper;
using System;
using System.Reflection;
using System.Windows;

namespace DGP.Snap.Window
{
    public static class WindowManager
    {
        private static WallpaperWindow _wallpaperWindow;
        public static WallpaperWindow WallpaperWindow
        {
            get
            {
                if (_wallpaperWindow == null||_wallpaperWindow.IsLoaded==false)
                {
                    _wallpaperWindow = new WallpaperWindow();
                }
                return _wallpaperWindow;
            }
            set
            {
                _wallpaperWindow = value;
            }

        }


        private static FrontSightWindow _frontSightWindow;
        public static FrontSightWindow FrontSightWindow
        {
            get
            {
                if (_frontSightWindow == null || _frontSightWindow.IsLoaded == false)
                {
                    _frontSightWindow = new FrontSightWindow();
                }
                return _frontSightWindow;
            }
        }
        public static bool IsFrontSightWindowShowing=false;

        /// <summary>
        /// 查找 <see cref="Application.Current.Windows"/> 中的对应 <see cref="TWindow"/> 类型的 Window
        /// </summary>
        /// <returns>未找到返回新实例</returns>
        public static System.Windows.Window FindWindow<TWindow>()
        {
            foreach(System.Windows.Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(TWindow))
                    return window;
            }
            return (System.Windows.Window)typeof(TWindow).Assembly.CreateInstance(typeof(TWindow).FullName);
        }
    }

}
