using DGP.Snap.Helper;
using DGP.Snap.Service.Download;
using DGP.Snap.Service.Kernel;
using DGP.Snap.Service.Shell;
using MahApps.Metro.Controls;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;

namespace DGP.Snap.Window.Wallpaper
{
    /// <summary>
    /// WallpaperWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WallpaperWindow : MetroWindow, INotifyPropertyChanged
    {
        public WallpaperWindow()
        {
            //单例解决了第一次打开窗口不加载的问题
            Singleton<WallpaperService>.Instance.InitializeAsync();
            ObservableWallpaperInfos = Singleton<WallpaperService>.Instance.WallpaperInfos;

            DataContext = this;

            InitializeComponent();
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            GC.Collect();
        }

        public ObservableCollection<Wallpaper> ObservableWallpaperInfos { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private Wallpaper _selected;

        public Wallpaper Selected
        {
            get { return _selected; }
            set
            {
                GCHelper.PerformAggressiveGC();
                Set(ref _selected, value);
            }
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "选择下载位置",
                //不使用InitialDirectory以记忆上次选择的位置
                //InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures),
                Filter = "jpg压缩图像文件| *.jpg"

            };
            saveFileDialog.ShowDialog();

            string path = saveFileDialog.FileName;
            if (path == "")
            {
                //todo 处理未选择，cancel
                return;
            }

            IFileDownloader fileDownloader = new FileDownloader();
            fileDownloader.DownloadFileCompleted += OnDownloadFileCompleted;
            fileDownloader.DownloadFileAsync(Selected.Uri, path);
        }

        private void OnDownloadFileCompleted(object sender, DownloadFileCompletedArgs e)
        {
            TrayIconManager.SystemNotificationManager.ShowNotification("Snap Desktop/壁纸", "壁纸下载完成！");
        }

        private void MetroWindow_WindowTransitionCompleted(object sender, RoutedEventArgs e)
        {
            Selected = ObservableWallpaperInfos[0];
        }
    }
}
