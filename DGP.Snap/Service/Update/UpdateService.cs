using DGP.Snap.Helper;
using DGP.Snap.Service.Download;
using DGP.Snap.Service.Shell;
using Microsoft.VisualBasic.Devices;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DGP.Snap.Service.Update
{
    class UpdateService
    {
        /// <summary>
        /// 不要在调用 <see cref="CheckUpdateAvailability()"/> 前使用，默认为<see cref="null"/>
        /// </summary>
        public static Uri PackageUri { get; set; } = null;

        /// <summary>
        /// 不要在调用 <see cref="CheckUpdateAvailability()"/> 前使用，默认为<see cref="null"/>
        /// </summary>
        public static Version NewVersion { get; set; } = null;

        public static Version CurrentVersion { get { return Assembly.GetExecutingAssembly().GetName().Version; } }
        public static async Task<UpdateAvailability> CheckUpdateAvailability()
        {
            try
            {
                Release release = await Json.GetWebRequestJsonObjectAsync<Release>("https://api.github.com/repos/DGP-Studio/DGP.Snap/releases/latest");

                var newVersion = release.Tag_name;
                NewVersion = new Version(release.Tag_name);

                if (new Version(newVersion) > CurrentVersion)//有新版本
                {
                    //Debug.WriteLine(newVersion);
                    PackageUri = new Uri(release.Assets[0].Browser_download_url);
                    return UpdateAvailability.NeedUpdate;
                }
                else
                {
                    if(new Version(newVersion) == CurrentVersion)
                    {
                        //最新发行版
                        return UpdateAvailability.IsNewestRelease;
                    }
                    else
                    {
                        //测试版
                        return UpdateAvailability.IsInsiderVersion;
                    }

                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return UpdateAvailability.NotAvailable;
            }
        }

        public static void DownloadAndInstallPackage()
        {
            UpdateProgressWindow = new UpdateProgressWindow();//弹出下载更新窗口
            UpdateProgressWindow.Show();

            IFileDownloader fileDownloader = new FileDownloader();
            fileDownloader.DownloadProgressChanged += OnDownloadProgressChanged;
            fileDownloader.DownloadFileCompleted += OnDownloadFileCompleted;
            
            string destinationPath = AppDomain.CurrentDomain.BaseDirectory + @"\Package.zip";
            //DirectorySecurity directorySecurity = new DirectorySecurity();
            //directorySecurity.AddAccessRule(new FileSystemAccessRule(@"Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
            //Directory.CreateDirectory(destinationPath);
            fileDownloader.DownloadFileAsync(PackageUri, destinationPath);
        }

        internal static void OnDownloadProgressChanged(object sender, DownloadFileProgressChangedArgs args)
        {
            double percent = Math.Round((double)args.BytesReceived/ args.TotalBytesToReceive*100,2);
            UpdateProgressWindow.ProgressBar.IsIndeterminate = false;
            UpdateProgressWindow.SetNewProgress(percent);
            UpdateProgressWindow.ProgressIndicatorText.Text = $@"{percent}% - {args.BytesReceived/1024}KB / {args.TotalBytesToReceive/1024}KB";
        }

        internal static void OnDownloadFileCompleted(object sender, DownloadFileCompletedArgs eventArgs)
        {
            if (eventArgs.State == CompletedState.Succeeded)
            {
                //download completed
                UpdateProgressWindow.SetNewProgress(100);
                UpdateProgressWindow.ProgressIndicatorText.Text = "下载已完成，请稍候...";
                Thread.Sleep(2000);
                //await Task.Run(() => { Thread.Sleep(2000); });

                UpdateProgressWindow.Close();
                StartUpdateInstall();

            }
            else if (eventArgs.State == CompletedState.Failed)
            {
                //download failed
            }
        }

        private static UpdateProgressWindow UpdateProgressWindow { get; set; } = null;

        public static void StartUpdateInstall()
        {
            if (File.Exists("OldUpdater.exe"))
                File.Delete("OldUpdater.exe");
            
            Computer MyComputer = new Computer();
            MyComputer.FileSystem.RenameFile(Path.GetFullPath("DGP.Snap.Updater.exe"), "OldUpdater.exe");

            Process.Start("OldUpdater.exe");
            Application.Current.Shutdown();
        }

        public static async Task HandleUpdateCheck()
        {
            UpdateAvailability updateAvailability = await CheckUpdateAvailability();
            switch (updateAvailability)
            {
                case UpdateAvailability.IsInsiderVersion:
                    TrayIconManager.SystemNotificationManager.ShowNotification("Snap Desktop", "开发版 Snap Desktop");
                    break;
                case UpdateAvailability.IsNewestRelease:
                    TrayIconManager.SystemNotificationManager.ShowNotification("Snap Desktop", "欢迎");
                    break;
                case UpdateAvailability.NeedUpdate:
                    TrayIconManager.SystemNotificationManager.ShowNotification("Snap Desktop", "单击此通知以下载可用更新...", () => { DownloadAndInstallPackage(); });
                    break;

                case UpdateAvailability.NotAvailable:
                    TrayIconManager.SystemNotificationManager.ShowNotification("Snap Desktop", "检查更新失败...");
                    break;
            }
        }
    }

}
