using DGP.Snap.Helper;
using DGP.Snap.Services.Shell;
using DGP.Snap.Services.Update;
using FileDownloader;
using Microsoft.VisualBasic.Devices;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DGP.Snap.Services.Update
{
    class UpdateService
    {
        /// <summary>
        /// 不要在调用 <see cref="CheckUpdateAvailability()"/> 前使用，默认为<see cref="null"/>
        /// </summary>
        public static Uri PackageUri { get; set; } = null;

        public static async Task<UpdateAvailability> CheckUpdateAvailability()
        {
            try
            {
                Release release = await Json.GetWebRequestJsonObject<Release>("https://api.github.com/repos/DGP-Studio/DGP.Snap/releases/latest");

                var newVersion = release.Tag_name;

                if (new Version(newVersion) > Assembly.GetExecutingAssembly().GetName().Version)//有新版本
                {
                    
                    Debug.WriteLine(newVersion);
                    PackageUri = new Uri(release.Assets[0].Browser_download_url);
                    return UpdateAvailability.NeedUpdate;

                }
                else
                {
                    if(new Version(newVersion) == Assembly.GetExecutingAssembly().GetName().Version)
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
            IFileDownloader fileDownloader = new FileDownloader.FileDownloader();
            fileDownloader.DownloadProgressChanged += OnDownloadProgressChanged;
            fileDownloader.DownloadFileCompleted += OnDownloadFileCompleted;

            string destinationPath = Environment.CurrentDirectory + @"\Package.zip";
            //DirectorySecurity directorySecurity = new DirectorySecurity();
            //directorySecurity.AddAccessRule(new FileSystemAccessRule(@"Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
            //Directory.CreateDirectory(destinationPath);
            fileDownloader.DownloadFileAsync(PackageUri, destinationPath);

            
        }

        internal static void OnDownloadProgressChanged(object sender, DownloadFileProgressChangedArgs args)
        {
            double percent = ((double)args.BytesReceived/ args.TotalBytesToReceive)*100;
            Debug.WriteLine("Downloaded {0}", percent);
        }

        internal static async void OnDownloadFileCompleted(object sender, DownloadFileCompletedArgs eventArgs)
        {
            if (eventArgs.State == CompletedState.Succeeded)
            {
                //download completed
                Debug.WriteLine("Downloaded Completed");
                TrayIconManager.NotificationManager.ShowNotification("Snap Desktop", "下载完成,开始安装");
                await Task.Run(() => { Thread.Sleep(3000); });


                StartUpdateInstall();
            }
            else if (eventArgs.State == CompletedState.Failed)
            {
                //download failed
            }
        }

        public static void StartUpdateInstall()
        {
            Computer MyComputer = new Computer();
            MyComputer.FileSystem.RenameFile(Path.GetFullPath("DGP.Snap.Updater.exe"), "OldUpdater.exe");
            
            Process.Start("OldUpdater.exe");
            Application.Current.Shutdown();
        }
    }

}
