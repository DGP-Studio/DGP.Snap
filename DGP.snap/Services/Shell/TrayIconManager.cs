using DGP.Snap.Properties;
using DGP.Snap.Services.Activation;
using DGP.Snap.Window;
using DGP.Snap.Window.Wallpaper;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DGP.Snap.Services.Shell
{
    internal class TrayIconManager : IDisposable
    {
        private static TrayIconManager _instance;

        private readonly NotifyIcon _notifyIcon;
        public NotifyIcon NotifyIcon => _notifyIcon;

        private readonly MenuItem _itemAutorun =
            new MenuItem("开机启动",
                (sender, e) =>
                {
                    if (AutoStartupHelper.IsAutorun())
                        AutoStartupHelper.SwitchAutoStartState(false);
                    else
                        AutoStartupHelper.SwitchAutoStartState(true);
                });


        private MenuItem _itemFrontSight =
            new MenuItem("辅助屏幕准星",
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


        private TrayIconManager()
        {
            _notifyIcon = new NotifyIcon
            {
                Text = "Snap Desktop",/*string.Format(TranslationHelper.Get("Icon_ToolTip"),*/
                Icon = GetTrayIconInResources(),
                Visible = true,
                ContextMenu = new ContextMenu(new[]
                {
                    new MenuItem($"Snap Desktop {Application.ProductVersion}") {Enabled = false},
                    //new MenuItem("检查更新", async (sender, e) => await UpdateService.CheckUpdateAvailability()),
                        //new MenuItem(TranslationHelper.Get("Icon_GetPlugin"),
                        //    (sender, e) => Process.Start("https://github.com/QL-Win/QuickLook/wiki/Available-Plugins")),
                        //_itemAutorun,
                    new MenuItem("-"),//分割线
                    new MenuItem("主页",(sender, e) => System.Windows.Application.Current.MainWindow.Show()),
                    new MenuItem("壁纸",(sender, e) => WindowManager.WallpaperWindow.Show()),
                    new MenuItem("-"),//分割线
                    _itemFrontSight,
                    new MenuItem("-"),//分割线
                    new MenuItem("退出",(sender, e) => System.Windows.Application.Current.Shutdown())
                })
            };

            _notifyIcon.ContextMenu.Popup += 
                (sender, e) => 
                {
                    _itemAutorun.Checked = AutoStartupHelper.IsAutorun();
                    _itemFrontSight.Checked = WindowManager.IsFrontSightWindowShowing;
                };//设置check


        }

        public void Dispose()
        {
            _itemAutorun.Dispose();
            _itemFrontSight.Dispose();
            NotifyIcon.Visible = false;
        }

        private Icon GetTrayIconInResources()
        {
            return Resources.Snapico;
        }

        public static class NotificationManager
        {
            public static void ShowNotification(string title, string content, bool isError = false, int timeout = 5000, Action clickEvent = null, Action closeEvent = null)
            {
                var icon = GetInstance().NotifyIcon;
                icon.ShowBalloonTip(timeout, title, content, ToolTipIcon.None);
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
        public static TrayIconManager GetInstance()
        {
            return _instance ?? (_instance = new TrayIconManager());
        }
    }
}
