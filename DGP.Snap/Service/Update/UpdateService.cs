using DGP.Snap.Helper;
using DGP.Snap.Service.Download;
using DGP.Snap.Service.Shell;
using DGP.Snap.Service.Update.Model;
using Microsoft.VisualBasic.Devices;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace DGP.Snap.Service.Update
{
    internal class UpdateService
    {
        /// <summary>
        /// 不要在调用 <see cref="CheckUpdateAvailabilityAsync()"/> 前使用，默认为<see cref="null"/>
        /// </summary>
        public Uri PackageUri { get; set; } = null;

        /// <summary>
        /// 不要在调用 <see cref="CheckUpdateAvailabilityAsync()"/> 前使用，默认为<see cref="null"/>
        /// </summary>
        public Version NewVersion { get; set; } = null;

        public Version CurrentVersion => Assembly.GetExecutingAssembly().GetName().Version;

        private async Task<UpdateAvailability> CheckUpdateAvailabilityAsync()
        {
            try
            {
                Release release = await Json.GetWebRequestObjectAsync<Release>("https://api.github.com/repos/DGP-Studio/DGP.Snap/releases/latest");

                var newVersion = release.TagName;
                NewVersion = new Version(release.TagName);

                if (new Version(newVersion) > CurrentVersion)//有新版本
                {
                    //Debug.WriteLine(newVersion);
                    PackageUri = new Uri(release.Assets[0].Browser_download_url);
                    return UpdateAvailability.NeedUpdate;
                }
                else
                {
                    if (new Version(newVersion) == CurrentVersion)
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
            catch (System.Exception e)
            {
                Debug.WriteLine(e.Message);
                return UpdateAvailability.NotAvailable;
            }
        }

        public void DownloadAndInstallPackage()
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

        internal void OnDownloadProgressChanged(object sender, DownloadFileProgressChangedArgs args)
        {
            double percent = Math.Round((double)args.BytesReceived / args.TotalBytesToReceive * 100, 2);
            UpdateProgressWindow.ProgressBar.IsIndeterminate = false;
            UpdateProgressWindow.SetNewProgress(percent);
            UpdateProgressWindow.ProgressIndicatorText.Text = $@"{percent}% - {args.BytesReceived / 1024}KB / {args.TotalBytesToReceive / 1024}KB";
        }

        internal void OnDownloadFileCompleted(object sender, DownloadFileCompletedArgs eventArgs)
        {
            if (eventArgs.State == CompletedState.Succeeded)
            {
                //download completed
                UpdateProgressWindow.SetNewProgress(100);
                UpdateProgressWindow.ProgressIndicatorText.Text = "下载已完成，请稍候...";

                //await Task.Run(() => { Thread.Sleep(2000); });

                UpdateProgressWindow.Close();
                StartUpdateInstall();

            }
            else if (eventArgs.State == CompletedState.Failed)
            {
                NotificationManager.ShowNotification("Snap Desktop", "在下载更新包时遇到问题");
                //download failed
            }
        }

        private UpdateProgressWindow UpdateProgressWindow { get; set; } = null;

        public static void StartUpdateInstall()
        {
            if (File.Exists("OldUpdater.exe"))
            {
                File.Delete("OldUpdater.exe");
            }

            new Computer().FileSystem.RenameFile(Path.GetFullPath("DGP.Snap.Updater.exe"), "OldUpdater.exe");

            Process.Start("OldUpdater.exe");
            Application.Current.Shutdown();
        }

        public async Task HandleUpdateCheckAsync(bool isStartupCheck = true)
        {
            UpdateAvailability updateAvailability = await CheckUpdateAvailabilityAsync();
            switch (updateAvailability)
            {
                case UpdateAvailability.IsInsiderVersion:
                    if (isStartupCheck)
                    {
                        NotificationManager.ShowNotification("Snap Desktop", $"{Environment.UserName}:\n你正在使用开发版 Snap Desktop");
                    }
                    else
                    {
                        NotificationManager.ShowNotification("Snap Desktop", "开发版 Snap Desktop ，不需要更新");
                    }

                    break;

                case UpdateAvailability.IsNewestRelease:
                    if (isStartupCheck)
                    {
                        NotificationManager.ShowNotification("Snap Desktop", $"欢迎{Environment.UserName}");
                    }
                    else
                    {
                        NotificationManager.ShowNotification("Snap Desktop", "不需要更新，这是 Snap Desktop 的最新发行版");
                    }

                    break;

                case UpdateAvailability.NeedUpdate:
                    NotificationManager.ShowNotification("Snap Desktop", $"发现可用的更新，版本号：{NewVersion}，单击此通知以下载...", () => { DownloadAndInstallPackage(); });
                    break;

                case UpdateAvailability.NotAvailable:
                    NotificationManager.ShowNotification("Snap Desktop", "检查更新失败...\n ·你的设备可能需要联网");
                    break;
            }
        }

        #region 单例
        private static UpdateService instance;
        private static object _lock = new object();
        private UpdateService()
        {

        }
        public static UpdateService GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new UpdateService();
                    }
                }
            }
            return instance;
        }
        #endregion
    }
}
