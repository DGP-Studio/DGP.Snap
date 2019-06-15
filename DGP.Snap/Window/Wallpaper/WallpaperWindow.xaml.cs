using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DGP.Snap.Window.Wallpaper
{
    /// <summary>
    /// WallpaperWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WallpaperWindow : System.Windows.Window
    {
        public WallpaperWindow()
        {
            InitializeComponent();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            GC.Collect();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //网络连接
            await WallPaperService.GetBingImageUriCollectionAsync();
            await WallPaperService.Get360ImageUriCollectionAsync();

            //List<ImageView> uIElementCollection = new List<ImageView>();
            //foreach(WallpaperInfo wallpaperInfo in WallPaperService.WallpaperInfos)
            //{
            //    uIElementCollection.Add(new ImageView(wallpaperInfo));
            //}
            //ImageGridView.ItemsSource = uIElementCollection;

            foreach (WallpaperInfo wallpaperInfo in WallPaperService.WallpaperInfos)
            {
                ImageGridView.Children.Add(new ImageView(wallpaperInfo));
            }

        }
    }
}
