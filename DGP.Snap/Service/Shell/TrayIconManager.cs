using DGP.Snap.Properties;
using DGP.Snap.Service.Update;
using DGP.Snap.Window;
using DGP.Snap.Window.Wallpaper;
using DGP.Snap.Window.LiveWallPaper;
using System;
using System.Windows.Forms;
using System.Diagnostics;
using DGP.Snap.Helper;
using DGP.Snap.Window.Weather;

namespace DGP.Snap.Service.Shell
{
    /// <summary>
    /// 托盘图标与其菜单管理类,此类自己实现单例与资源回收
    /// </summary>
    internal class TrayIconManager : IDisposable
    {
        private MenuItem MenuItemSeparator { get { return new MenuItem("-"); } }

        public NotifyIcon NotifyIcon { get; }

        private readonly MenuItem _itemAutorun =
            new MenuItem("开机启动",
            (sender, e) =>
            {
                if (AutoStartupHelper.IsAutorun())
                    AutoStartupHelper.SetAutoStartState(false);
                else
                    AutoStartupHelper.SetAutoStartState(true);
            });

        private string AppDebugOrRelease
        {
            get
            {
#if DEBUG
                if(Debugger.IsAttached)
                    return "[DEBUG]-DEBUGGING";
                return "[DEBUG]";
#else
                return "[正式版]";
#endif
            }
        }

        /// <summary>
        /// private的原因是为了防止利用 <see cref="TrayIconManager()"/> 构造函数生成多个实例
        /// </summary>
        private TrayIconManager()
        {
            NotifyIcon = new NotifyIcon
            {
                Text = "Snap Desktop",
                Icon = Resources.SnapNewIcon,
                Visible = true,
                ContextMenu = new ContextMenu(new[]
                {
                    //修改MenuItem的OwnerDraw属性可以自定义外观
                    new MenuItem($"Snap Desktop {Application.ProductVersion}") { Enabled = false },
                    new MenuItem($"{AppDebugOrRelease}") { Enabled = false },
                    new MenuItem(
                        "更新",
                        new[] {
                            new MenuItem("检查更新", async (sender, e) => await Singleton<UpdateService>.Instance.HandleUpdateCheck(false)),
                            new MenuItem("手动下载", (sender, e) => Process.Start("https://github.com/DGP-Studio/DGP.Snap/releases")),
                        }),
                    _itemAutorun,//自动启动
                    MenuItemSeparator,
                    new MenuItem("操作中心", (sender, e) => WindowManager.GetOrAddNormalWindow<MainWindow>().Show()),
                    MenuItemSeparator,
                    new MenuItem("壁纸", (sender, e) => WindowManager.GetOrAddNormalWindow<WallpaperWindow>().Show()),
                    MenuItemSeparator,
                    new MenuItem(
                        "桌面部件",
                        new[] {
                            new MenuItem("动态壁纸", (sender, e) => WindowManager.AddUIelementToTileWindow(new LiveWallPaperView(),0,0,0)),
                            new MenuItem("天气",(sender,e)=>WindowManager.GetOrAddNormalWindow<WeatherTileWindow>().Show())
                        }),
                    //MenuItemSeparator,
                    //_itemFrontSight,
                    MenuItemSeparator,
                    new MenuItem("退出", (sender, e) => System.Windows.Application.Current.Shutdown()),
                })
            };

            NotifyIcon.ContextMenu.Popup +=
                (sender, e) =>
                {
                    _itemAutorun.Checked = AutoStartupHelper.IsAutorun();
                };//设置check
        }
        /// <summary>
        /// 实现 <see cref="IDisposable"/> 接口
        /// </summary>
        public void Dispose()
        {
            _itemAutorun.Dispose();
            NotifyIcon.Visible = false;
        }

        public class SystemNotificationManager
        {
            /// <summary>
            /// 显示经典Windows系统通知
            /// </summary>
            /// <param name="title">显示的标题，一般为 Snap Desktop</param>
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
