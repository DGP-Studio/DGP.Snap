using DGP.Snap.Helper;
using DGP.Snap.Helper.Extensions;
using DGP.Snap.Properties;
using DGP.Snap.Service.Setting;
using DGP.Snap.Service.Update;
using DGP.Snap.Window;
using DGP.Snap.Window.Side;
using DGP.Snap.Window.Wallpaper;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace DGP.Snap.Service.Shell
{
    /// <summary>
    /// 托盘图标与其菜单管理类,此类自身实现单例与资源回收
    /// </summary>
    internal class TrayIconManager : IDisposable
    {

        private MenuItem MenuItemSeparator { get { return new MenuItem("-"); } }

        public NotifyIcon NotifyIcon { get; }

        private readonly MenuItem itemAutorun =
            new MenuItem("开机启动",
            (sender, e) =>
            {
                if (AutoStartupHelper.IsAutorun())
                {
                    AutoStartupHelper.SetAutoStartState(false);
                }
                else
                {
                    AutoStartupHelper.SetAutoStartState(true);
                }
            });

        public string AppDebugOrRelease
        {
            get
            {
                #if DEBUG
                if (Debugger.IsAttached)
                {
                    return "[DEBUG]-DEBUGGING";
                }

                return "[DEBUG]";
                #else
                return "[BETA VERSION]";
                #endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private TrayIconManager()
        {
            NotifyIcon = new NotifyIcon
            {
                Text = "Snap Desktop",
                Icon = Resources.SnapNewIcon,
                Visible = true,
                ContextMenu = new ContextMenu(new[]
                {
                    //版本号
                    new MenuItem($"Snap Desktop {Application.ProductVersion}") { Enabled = false },
                    //发行或测试
                    new MenuItem($"{AppDebugOrRelease}") { Enabled = false },
                    new MenuItem("更新",
                        new[] {
                        new MenuItem("检查更新", async (sender, e) => await UpdateService.GetInstance().HandleUpdateCheck(false)),
                        new MenuItem("手动下载", (sender, e) => Process.Start("https://github.com/DGP-Studio/DGP.Snap/releases")),
                        }),
                    itemAutorun,//自动启动
                    MenuItemSeparator,
                    new MenuItem("操作中心", (sender, e) => WindowManager.GetOrAddNormalWindow<MainWindow>().Show()),
                    MenuItemSeparator,
                    new MenuItem("壁纸", (sender, e) => WindowManager.GetOrAddNormalWindow<WallpaperWindow>().Show()),
                    MenuItemSeparator,
                    new MenuItem("退出", async (sender, e) =>
                    {
                        await Task.Run(async() => 
                        await SettingService.GetInstance().SaveSettingsAsync())
                        .ContinueWith(async(t) => 
                        await Task.Run(()=>
                        App.Current.Dispatcher.Invoke(()=>
                        App.Current.Shutdown()
                        )));
                    })
                })
            };

            NotifyIcon.Click +=
                (sender, e) =>
                {
                    if (((MouseEventArgs)e).Button == MouseButtons.Left)
                    {
                        System.Windows.Window sideWindow = WindowManager.GetOrAddNormalWindow<SideWindow>();
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
                    itemAutorun.Checked = AutoStartupHelper.IsAutorun();
                };

        }

        #region 单例
        private static TrayIconManager instance;
        private static object _lock = new object();
        public static TrayIconManager GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new TrayIconManager();
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
