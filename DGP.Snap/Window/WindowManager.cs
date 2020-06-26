using DGP.Snap.Helper;
using System.Windows;
using System.Windows.Controls;

namespace DGP.Snap.Window
{
    public class WindowManager
    {
        /// <summary>
        /// 查找 <see cref="Application.Current.Windows"/> 集合中的对应 <typeparamref name="TWindow"/> 类型的 Window
        /// </summary>
        /// <returns>返回唯一的窗口，未找到返回新实例</returns>
        public static System.Windows.Window GetOrAddNormalWindow<TWindow>() where TWindow : System.Windows.Window
        {
            foreach (System.Windows.Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(TWindow))
                {
                    return window;
                }
            }
            return (System.Windows.Window)typeof(TWindow).Assembly.CreateInstance(typeof(TWindow).FullName);
            //return Singleton<TWindow>.Instance as System.Windows.Window;
        }
    }

}
