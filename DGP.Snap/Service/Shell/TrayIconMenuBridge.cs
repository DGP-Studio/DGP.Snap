using DGP.Snap.Helper;
using DGP.Snap.Service.Update;
using DGP.Snap.Window;
using DGP.Snap.Window.LiveWallpaper;
using DGP.Snap.Window.Wallpaper;
using DGP.Snap.Window.Weather;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace DGP.Snap.Service.Shell
{
    class TrayIconMenuBridge
    {
        private static ContextMenu ContextMenu = new ContextMenu() {BorderThickness= new Thickness(0) };

        private TrayIconMenu TrayIconMenu = new TrayIconMenu();

        private static readonly MenuItem _itemAutorun =
            new MenuItem("开机启动",
            (sender, e) =>
            {
                if (AutoStartupHelper.IsAutorun())
                    AutoStartupHelper.SetAutoStartState(false);
                else
                    AutoStartupHelper.SetAutoStartState(true);
            });

        public static object GetMenu()
        {
            _itemAutorun.Checked = AutoStartupHelper.IsAutorun();
            ContextMenu.Items.Clear();
            UIElement[] menuItems =
            {
                new Separator(){ Visibility=Visibility.Hidden,Height=2 },
                //版本号
                new MenuItem($"Snap Desktop {System.Windows.Forms.Application.ProductVersion}") { Enabled = false },
                //发行或测试
                new MenuItem($"{TrayIconManager.Instance.AppDebugOrRelease}") { Enabled = false },
                new MenuItem("更新",
                    new[] {
                        new MenuItem("检查更新", async (sender, e) => await Singleton<UpdateService>.Instance.HandleUpdateCheck(false)),
                        new MenuItem("手动下载", (sender, e) => Process.Start("https://github.com/DGP-Studio/DGP.Snap/releases")),
                    }),
                _itemAutorun,//自动启动
                new Separator(),
                new MenuItem("操作中心", (sender, e) => WindowManager.GetOrAddNormalWindow<MainWindow>().Show()),
                new Separator(),
                new MenuItem("壁纸", (sender, e) => WindowManager.GetOrAddNormalWindow<WallpaperWindow>().Show()),
                new Separator(),
                new MenuItem("桌面部件",
                    new[] {
                        new MenuItem("视频壁纸BETA", (sender, e) => WindowManager.AddUIelementToTileWindow(new LiveWallPaperView(), 0, 0, 0)),
                        new MenuItem("代码雨", (sender, e) => WindowManager.AddUIelementToTileWindow(new LiveMatrixRainView(), 0, 0, 0)),
                        new MenuItem("天气", (sender, e) => WindowManager.GetOrAddNormalWindow<WeatherTileWindow>().Show())
                    }),
                new Separator(),
                new MenuItem("退出", (sender, e) => Application.Current.Shutdown()),
                new Separator(){ Visibility=Visibility.Hidden,Height=2 },
                
            };
            foreach (UIElement item in menuItems)
                ContextMenu.Items.Add(item);
            return ContextMenu;
        }
    }
    /// <summary>
    /// 重写并继承了<see cref="System.Windows.Controls.MenuItem"/>以实现WinForm的MenuItem特有功能
    /// </summary>
    internal class MenuItem : System.Windows.Controls.MenuItem
    {
        private TrayIconMenu TrayIconMenu = new TrayIconMenu();
        public MenuItem()
        {
            this.Style= TrayIconMenu.GetStyle("StandardMenuItemStyle") as Style;
        }
        public MenuItem(string text):this()
        {
            Header = text;
        }
        public MenuItem(string text, RoutedEventHandler onClick):this()
        {
            Header = text;
            Click += onClick;
        }
        public MenuItem(string text, MenuItem[] items):this()
        {
            Header = text;
            foreach (MenuItem item in items)
                Items.Add(item);

        }
        public bool Enabled
        {
            get
            {
                return IsEnabled;
            }
            set
            {
                IsEnabled = value;
            }
        }
        public new bool Checked
        {
            get
            {
                return IsChecked;
            }
            set
            {
                IsChecked = value;
            }
        }
    }
} 
