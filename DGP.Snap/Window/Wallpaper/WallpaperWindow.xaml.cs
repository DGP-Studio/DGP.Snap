using DGP.Snap.Helper;
using DGP.Snap.Helper.Extensions;
using DGP.Snap.Service.Download;
using DGP.Snap.Service.Kernel;
using DGP.Snap.Service.Setting;
using DGP.Snap.Service.Shell;
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
    public partial class WallpaperWindow : System.Windows.Window, INotifyPropertyChanged
    {
        public WallpaperWindow()
        {
            //单例解决了第一次打开窗口不加载的问题
            //并不能保证窗口加载图片
            WallpaperService.GetInstance().InitializeAsync();
            ObservableWallpaperInfos = WallpaperService.GetInstance().WallpaperInfos;

            DataContext = this;

            InitializeComponent();
            //Selected = ObservableWallpaperInfos[0];
        }

        protected override async void OnClosing(CancelEventArgs e)
        {
            await SettingService.GetInstance().SaveSettingsAsync();
            GC.Collect();
        }

        public ObservableCollection<Wallpaper> ObservableWallpaperInfos { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        #region INotifyPropertyChanged
        private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }
            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

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
                Filter = "jpeg压缩图像文件| *.jpg"

            };
            saveFileDialog.ShowDialog();

            string path = saveFileDialog.FileName;
            if (path == "")
            {
                return;
            }
            //不能using
            IFileDownloader fileDownloader = new FileDownloader();
            fileDownloader.DownloadFileCompleted += OnDownloadFileCompleted;
            fileDownloader.DownloadFileAsync(Selected.Uri, path);
        }

        private void OnDownloadFileCompleted(object sender, DownloadFileCompletedArgs e)
        {
            if (e.State == CompletedState.Succeeded)
            {
                NotificationManager.ShowNotification("Snap Desktop/壁纸", ":)\n壁纸下载完成！");
            }
            else
            {
                NotificationManager.ShowNotification("Snap Desktop/壁纸", ":(\n未能正常下载");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetAcrylicblur();
            //处理速度慢的计算机不能正常的给ObservableWallpaperInfos赋值
            //需要额外添加判断
            if (ObservableWallpaperInfos.Count > 0)
            {
                Selected = ObservableWallpaperInfos[0];
            }
            else
            {
                NotificationManager.ShowNotification("Snap Desktop/壁纸", "加载壁纸时遇到问题\n请手动选择你想要查看的壁纸");
                Selected = null;
            }
        }

        private void Win_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                BorderGrid.Margin = new Thickness(7);
            }
            else//最大化
            {
                BorderGrid.Margin = new Thickness(0);
            }
        }

    }
}
