using DGP.Snap.Services.Shell;
using DGP.Snap.Services.Update;
using DGP.Snap.Window.Wallpaper;
using FileDownloade;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Windows;

namespace DGP.Snap
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {


        protected override async void OnStartup(StartupEventArgs e)
        {
            //托盘图标
            TrayIconManager.GetInstance();

            //更新
            UpdateAvailability updateAvailability = await UpdateService.CheckUpdateAvailability();

            switch (updateAvailability)
            {
                case UpdateAvailability.IsInsiderVersion:
                    TrayIconManager.NotificationManager.ShowNotification("Snap Desktop", "你正在运行 Snap Desktop 的测试版本");
                    break;
                case UpdateAvailability.IsNewestRelease:
                    TrayIconManager.NotificationManager.ShowNotification("Snap Desktop", "正常启动");
                    break;
                case UpdateAvailability.NeedUpdate:
                    TrayIconManager.NotificationManager.ShowNotification("Snap Desktop", "单击此通知以下载可用更新...", false, 5000, () => { UpdateService.DownloadAndInstallPackage(); });
                    break;

                case UpdateAvailability.NotAvailable:
                    TrayIconManager.NotificationManager.ShowNotification("Snap Desktop", "检查更新失败...");
                    break;
            }

            base.OnStartup(e);
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Debug.WriteLine(e.ExceptionObject.ToString());
        }

        //public static readonly string AppFullPath = Assembly.GetExecutingAssembly().Location;

        protected override void OnExit(ExitEventArgs e)
        {
            TrayIconManager.GetInstance().Dispose();
            Debug.WriteLine("正常终止!");
            base.OnExit(e);
        }
    }
}
