using DGP.Snap.Properties;
using DGP.Snap.Service.Update;
using DGP.Snap.Window;
using DGP.Snap.Window.Wallpaper;
using DGP.Snap.Window.Weather;
using System;
using System.Windows.Forms;

namespace DGP.Snap.Service.Shell
{
    /// <summary>
    /// 托盘图标与其菜单管理类,此类自己实现单例与资源回收
    /// </summary>
    internal class TrayIconManager : IDisposable
    {
        private MenuItem MenuItemSeparator { get { return new MenuItem("-"); } }



        private readonly NotifyIcon _notifyIcon;
        public NotifyIcon NotifyIcon => _notifyIcon;

        private readonly MenuItem _itemAutorun =
            new MenuItem("开机启动",
            (sender, e) =>
            {
                if (AutoStartupHelper.IsAutorun())
                    AutoStartupHelper.SetAutoStartState(false);
                else
                    AutoStartupHelper.SetAutoStartState(true);
            });


        private MenuItem _itemFrontSight =
            new MenuItem("屏幕准星",
            (sender, e)=>{
                if (WindowManager.FrontSightWindow?.IsLoaded==null||WindowManager.FrontSightWindow?.IsLoaded==false)
                {
                    WindowManager.FrontSightWindow.Show();
                }
                else
                {
                    WindowManager.FrontSightWindow.Close();
                }
            });

        /// <summary>
        /// private的原因是为了防止利用 <see cref="TrayIconManager()"/> 构造函数生成多个实例
        /// </summary>
        private TrayIconManager()
        {
            _notifyIcon = new NotifyIcon
            {
                Text = "Snap Desktop",/*string.Format(TranslationHelper.Get("Icon_ToolTip"),*/
                Icon = Resources.SnapNewIcon,
                Visible = true,
                ContextMenu = new ContextMenu(new[]
                {
                    //修改MenuItem的OwnerDraw属性可以自定义外观
                    new MenuItem($"Snap Desktop {Application.ProductVersion}") { Enabled = false },
                    new MenuItem("检查更新", async (sender, e) => await UpdateService.HandleUpdateCheck()),
                    //new MenuItem(TranslationHelper.Get("Icon_GetPlugin"),
                    //    (sender, e) => Process.Start("https://github.com/QL-Win/QuickLook/wiki/Available-Plugins")),
                    _itemAutorun,
                    MenuItemSeparator,
                    new MenuItem("操作中心", (sender, e) => WindowManager.GetOrAddNormalWindow<MainWindow>().Show()/*System.Windows.Application.Current.MainWindow.Show()*/),
                    MenuItemSeparator,
                    new MenuItem("壁纸", (sender, e) => WindowManager.GetOrAddNormalWindow<WallpaperWindow>().Show()),
                    MenuItemSeparator,
                    new MenuItem(
                        "桌面磁贴",
                        new[] {
                            new MenuItem("天气", (sender, e) => WindowManager.DesktopBottomMostCanvas.Children.Add(new WeatherView())),
                        }),
                    
                    MenuItemSeparator,
                    _itemFrontSight,
                    MenuItemSeparator,
                    new MenuItem("退出", (sender, e) => System.Windows.Application.Current.Shutdown()),
                })
            };

            _notifyIcon.ContextMenu.Popup += 
                (sender, e) => 
                {
                    _itemAutorun.Checked = AutoStartupHelper.IsAutorun();
                    _itemFrontSight.Checked = WindowManager.IsFrontSightWindowShowing;
                };//设置check
        }
        /// <summary>
        /// 实现 <see cref="IDisposable"/> 接口
        /// </summary>
        public void Dispose()
        {
            _itemAutorun.Dispose();
            _itemFrontSight.Dispose();
            NotifyIcon.Visible = false;
        }

        public class SystemNotificationManager
        {
            /// <summary>
            /// 显示经典Windows系统通知
            /// </summary>
            /// <param name="title">显示的标题，一般为 Snap Desktop。</param>
            /// <param name="content">显示的内容</param>
            /// <param name="clickEvent">点击通知触发的<see cref="Action"/></param>
            /// <param name="closeEvent">通知消失时触发的<see cref="Action"/></param>
            public static void ShowNotification(string title, string content, Action clickEvent = null, Action closeEvent = null)
            {
                var icon = Instance().NotifyIcon;
                icon.ShowBalloonTip(5000, title, content, ToolTipIcon.None);
                icon.BalloonTipClicked += OnIconOnBalloonTipClicked;
                icon.BalloonTipClosed += OnIconOnBalloonTipClosed;

                void OnIconOnBalloonTipClicked(object sender, EventArgs e)
                {
                    clickEvent?.Invoke();
                    icon.BalloonTipClicked -= OnIconOnBalloonTipClicked;
                }

                void OnIconOnBalloonTipClosed(object sender, EventArgs e)
                {
                    closeEvent?.Invoke();
                    icon.BalloonTipClosed -= OnIconOnBalloonTipClosed;
                }
            }

        }

        private static TrayIconManager _instance;

        /// <summary>
        /// 获取<see cref="TrayIconManager"/>的实例
        /// </summary>
        /// <returns></returns>
        public static TrayIconManager Instance()
        {
            return _instance ?? (_instance = new TrayIconManager());
        }
    }
}
