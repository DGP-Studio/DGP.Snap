using DGP.Snap.Helper;
using DGP.Snap.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace DGP.Snap.Service.Shell
{
    /// <summary>
    /// 托盘图标与其菜单管理类,此类自身实现单例与资源回收
    /// </summary>
    internal class TrayIconManager : IDisposable, INotNewable
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

        public string AppDebugOrRelease
        {
            get
            {
#if DEBUG
                if (Debugger.IsAttached)
                    return "[DEBUG]-DEBUGGING";
                return "[DEBUG]";
#else
                return "[BETA VERSION]";
#endif
            }
        }

        /// <summary>
        /// private的原因是为了防止利用 <see cref="TrayIconManager()"/> 构造函数生成多个实例
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private TrayIconManager()
        {
            NotifyIcon = new NotifyIcon
            {
                Text = "Snap Desktop",
                Icon = Resources.SnapNewIcon,
                Visible = true,
                ContextMenu = new ContextMenu(),
            };

            NotifyIcon.Click +=
                (sender, e) =>
                {
                    if (((MouseEventArgs)e).Button== MouseButtons.Right)
                    {
                        System.Windows.Controls.ContextMenu menu = (System.Windows.Controls.ContextMenu)TrayIconMenuBridge.GetMenu();
                        menu.IsOpen = true;
                    }
                };

            //NotifyIcon.ContextMenu.Popup +=
            //    (sender, e) =>
            //    {
            //        _itemAutorun.Checked = AutoStartupHelper.IsAutorun();
            //    };//设置check

        }
        /// <summary>
        /// 实现 <see cref="IDisposable"/> 接口
        /// </summary>
        public void Dispose()
        {
            //_itemAutorun.Dispose();
            NotifyIcon.Visible = false;
        }

        public class SystemNotificationManager
        {
            /// <summary>
            /// 显示Windows系统通知
            /// </summary>
            /// <param name="title">显示的标题，一般为 Snap Desktop</param>
            /// <param name="content">显示的内容</param>
            /// <param name="clickEvent">点击通知触发的<see cref="Action"/></param>
            /// <param name="closeEvent">通知消失时触发的<see cref="Action"/></param>
            /// <param name="timedout">通知显示的时间，以毫秒为单位</param>
            public static void ShowNotification(string title, string content, Action clickEvent = null, Action closeEvent = null, int timedout = 3000)
            {
                var icon = Instance.NotifyIcon;
                icon.ShowBalloonTip(timedout, title, content, ToolTipIcon.None);
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
        public static TrayIconManager Instance
        {
            get
            {
                return _instance ?? (_instance = new TrayIconManager());
            }
        }
    }

}
