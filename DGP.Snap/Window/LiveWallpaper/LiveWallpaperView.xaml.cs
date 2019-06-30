using DGP.Snap.Helper;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Interop;

namespace DGP.Snap.Window.LiveWallPaper
{
    /// <summary>
    /// WeatherView.xaml 的交互逻辑
    /// </summary>
    public partial class LiveWallPaperView : System.Windows.Controls.UserControl
    {
        public LiveWallPaperView()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "选择本地视频",
                Filter = "mp4视频文件| *.mp4|所有文件|*.*"
            };
            openFileDialog.ShowDialog();

            string path = openFileDialog.FileName;
            if (path != "")
                VideoSource = new Uri(path);
            else
                VideoSource = null;

            DataContext = this;

            InitializeComponent();
            Width = SystemParameters.PrimaryScreenWidth;
            Height = SystemParameters.PrimaryScreenHeight;

            GC.KeepAlive(VideoSource);
        }

        public Uri VideoSource
        {
            get { return (Uri)GetValue(VideoSourceProperty); }
            set { SetValue(VideoSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WeatherInfo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VideoSourceProperty =
            DependencyProperty.Register("VideoSource", typeof(Uri), typeof(LiveWallPaperView));

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
                //var hwndSource = PresentationSource.FromVisual(this) as HwndSource;
                //var hwndTarget = hwndSource.CompositionTarget;
                //hwndTarget.RenderMode = RenderMode.SoftwareOnly;
        }
    }
}
