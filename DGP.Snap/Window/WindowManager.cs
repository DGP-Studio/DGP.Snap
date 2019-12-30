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
                    return window;
            }
            return (System.Windows.Window)typeof(TWindow).Assembly.CreateInstance(typeof(TWindow).FullName);
            //return Singleton<TWindow>.Instance as System.Windows.Window;
        }

        public static void AddUIelementToTileWindow(UIElement uIElement,double left,double top,int zIndex)
        {
            Singleton<TileWindow>.Instance.Show();
            Singleton<TileWindow>.Instance.Presenter.Children.Add(uIElement);

            uIElement.SetValue(Panel.ZIndexProperty, zIndex);
            uIElement.SetValue(Canvas.LeftProperty, left);
            uIElement.SetValue(Canvas.TopProperty, top);
        }
        /// <summary>
        /// 处理桌面磁贴类别的菜单项点击事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="checkflag">设置菜单项的check</param>
        public static void DesktopTileWindowMenuItemClick(System.Windows.Forms.MenuItem sender, RoutedEventArgs e, bool? checkflag = null)
        {

        }


    }

}
