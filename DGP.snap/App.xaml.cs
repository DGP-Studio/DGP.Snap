using DGP.Snap.Helper;
using DGP.Snap.Service.Shell;
using DGP.Snap.Service.Update;
using DGP.Snap.Window.Weather;
using System;
using System.Diagnostics;
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
                    TrayIconManager.SystemNotificationManager.ShowNotification("Snap Desktop", "开发版 Snap Desktop");
                    break;
                case UpdateAvailability.IsNewestRelease:
                    TrayIconManager.SystemNotificationManager.ShowNotification("Snap Desktop", "欢迎");
                    break;
                case UpdateAvailability.NeedUpdate:
                    TrayIconManager.SystemNotificationManager.ShowNotification("Snap Desktop", "单击此通知以下载可用更新...", () => { UpdateService.DownloadAndInstallPackage(); });
                    break;

                case UpdateAvailability.NotAvailable:
                    TrayIconManager.SystemNotificationManager.ShowNotification("Snap Desktop", "检查更新失败...");
                    break;
            }

            base.OnStartup(e);
            
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Debug.WriteLine(e.ExceptionObject.ToString());
        }

        protected override void OnExit(ExitEventArgs e)
        {
            TrayIconManager.GetInstance().Dispose();
            Debug.WriteLine("正常终止!");
            base.OnExit(e);
        }
    }
}
