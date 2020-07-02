using DGP.Snap.Properties;
using DGP.Snap.Service.LifeCycle;
using DGP.Snap.Service.Update;
using DGP.Snap.Window;
using DGP.Snap.Window.Side;
using DGP.Snap.Window.Wallpaper;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace DGP.Snap.Service.Shell
{
    /// <summary>
    /// 托盘图标与其菜单管理类,此类自身实现单例与资源回收
    /// </summary>
    internal class NotifyIconManager : IDisposable
    {
        private MenuItem MenuItemSeparator => new MenuItem("-");

        public NotifyIcon NotifyIcon { get; }

        private readonly MenuItem itemAutorun =
            new MenuItem("开机启动",
            (sender, e) =>
            {
                if (AutoStartupService.IsAutorun())
                {
                    AutoStartupService.SetAutoStartState(false);
                }
                else
                {
                    AutoStartupService.SetAutoStartState(true);
                }
            });

        private NotifyIconManager()
        {
            NotifyIcon = new NotifyIcon
            {
                Text = "Snap Desktop",
                Icon = Resources.SnapNewIcon,
                Visible = true,
                ContextMenu = new ContextMenu(new[]
                {
                    new MenuItem("壁纸", (sender, e) => WindowManager.GetOrAddWindow<WallpaperWindow>().Show()),
                    MenuItemSeparator,

                    new MenuItem("更新",new[] {
                        new MenuItem("检查更新", async (sender, e) => await UpdateService.GetInstance().HandleUpdateCheckAsync(false)),
                        new MenuItem("手动下载", (sender, e) => Process.Start("https://github.com/DGP-Studio/DGP.Snap/releases")),}),
                    itemAutorun,//自动启动
                    MenuItemSeparator,
                    new MenuItem("关于", (sender, e) => WindowManager.GetOrAddWindow<AboutWindow>().Show()),
                    new MenuItem("退出", (sender, e) => LifeCycleService.Application_ExitAsync())
                })
            };

            NotifyIcon.Click +=
                (sender, e) =>
                {
                    if (((MouseEventArgs)e).Button == MouseButtons.Left)
                    {
                        System.Windows.Window sideWindow = WindowManager.GetOrAddWindow<SideWindow>();
                        if (sideWindow.IsVisible)
                        {
                            sideWindow.Close();
                        }
                        else
                        {
                            sideWindow.Show();
                        }
                    }
                };
            //设置check
            NotifyIcon.ContextMenu.Popup +=
                (sender, e) =>
                {
                    itemAutorun.Checked = AutoStartupService.IsAutorun();
                };

        }

        #region 单例
        private static NotifyIconManager instance;
        private static object _lock = new object();
        public static NotifyIconManager GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new NotifyIconManager();
                    }
                }
            }
            return instance;
        }
        #endregion

        /// <summary>
        /// 实现 <see cref="IDisposable"/> 接口
        /// </summary>
        public void Dispose()
        {
            //_itemAutorun.Dispose();
            NotifyIcon.Visible = false;
        }
    }

}
