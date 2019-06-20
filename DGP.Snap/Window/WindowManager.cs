using DGP.Snap.Helper;
using DGP.Snap.Window.FrontSight;
using DGP.Snap.Window.Wallpaper;
using System;
using System.Reflection;
using System.Windows;

namespace DGP.Snap.Window
{
    public static class WindowManager
    {


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
        /// 查找 <see cref="Application.Current.Windows"/> 中的对应 <typeparamref name="TWindow"/> 类型的 Window
        /// </summary>
        /// <returns>未找到返回新实例</returns>
        public static System.Windows.Window GetOrAddNormalWindow<TWindow>() where TWindow : System.Windows.Window//new()
        {
            foreach (System.Windows.Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(TWindow))
                    return window;
            }
            return (System.Windows.Window)typeof(TWindow).Assembly.CreateInstance(typeof(TWindow).FullName);
            //return Singleton<TWindow>.Instance as System.Windows.Window;
        }


    }

}
