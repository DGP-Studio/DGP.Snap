using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DGP.Snap.Window.Wallpaper
{
    /// <summary>
    /// ImageView.xaml 的交互逻辑
    /// </summary>
    public partial class ImageView : UserControl,INotifyPropertyChanged
    {
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

        /// <summary>
        /// 此构造函数不应使用，应使用 <see cref="ImageView.ImageView(WallpaperInfo wallpaperinfo)"/>
        /// </summary>
        public ImageView()
        {
            InitializeComponent();
            DataContext = this;
        }
        public ImageView(WallpaperInfo wallpaperinfo)
        {
            InitializeComponent();
            WallpaperInfo = wallpaperinfo;
            Image.Source = new BitmapImage(wallpaperinfo.ThumbnailUri);
            DescriptionText.Text = wallpaperinfo.Description;
        }
        public static readonly DependencyProperty WallpaperInfoProperty =
            DependencyProperty.Register("WallpaperInfo", typeof(WallpaperInfo), typeof(ImageView),null);
        public WallpaperInfo WallpaperInfo
        {
            get { return (WallpaperInfo)GetValue(WallpaperInfoProperty); }
            set { SetValue(WallpaperInfoProperty, value); }
        }
    }
}
