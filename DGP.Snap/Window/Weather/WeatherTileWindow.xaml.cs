using DGP.Snap.Helper;
using DGP.Snap.Helper.Extensions;
using System;
using System.Collections.Generic;
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

namespace DGP.Snap.Window.Weather
{
    /// <summary>
    /// WeatherTileWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WeatherTileWindow : System.Windows.Window
    {
        public WeatherTileWindow()
        {
            
            DataContext=this;
            InitializeComponent();
            this.SetBottomWithInteractivity();
        }
        //保持置底状态
        private void HandleWindowBottomState(object sender, EventArgs e) => this.SetBottomWithInteractivity();

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsPinned == Visibility.Hidden)
            {
                DragMove();
            }
            
        }

        public WeatherModel WeatherInfo
        {
            get { return (WeatherModel)GetValue(WeatherInfoProperty); }
            set { SetValue(WeatherInfoProperty, value); }
        }
        // Using a DependencyProperty as the backing store for WeatherInfo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WeatherInfoProperty =
            DependencyProperty.Register("WeatherInfo", typeof(WeatherModel), typeof(WeatherTileWindow));

        private async void Window_Loaded(object sender, RoutedEventArgs e)//不用异步方法获取天气会出错
        {
            await Singleton<WeatherService>.Instance.InitializeAsync();
            WeatherInfo = Singleton<WeatherService>.Instance.WeatherInformation;
        }

        private async void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                WeatherInfo = await Singleton<WeatherService>.Instance.GetRefreshedWeatherAsync(((TextBox)sender).Text);
            }
        }



        public Visibility IsPinned
        {
            get { return (Visibility)GetValue(IsPinnedProperty); }
            set { SetValue(IsPinnedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsPinned.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPinnedProperty =
            DependencyProperty.Register("IsPinned", typeof(Visibility), typeof(WeatherTileWindow), new PropertyMetadata(Visibility.Hidden));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsPinned == Visibility.Hidden)
                IsPinned = Visibility.Visible;
            else
                IsPinned = Visibility.Hidden;
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            WeatherInfo = await Singleton<WeatherService>.Instance.GetRefreshedWeatherAsync(WeatherInfo.City);
        }
    }
}
