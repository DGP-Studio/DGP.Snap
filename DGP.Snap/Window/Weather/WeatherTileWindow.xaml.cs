using DGP.Snap.Helper;
using DGP.Snap.Helper.Extensions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            if(e.LeftButton==MouseButtonState.Pressed&&e.RightButton==MouseButtonState.Released)
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
            IsPinned = IsPinned == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden;
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            WeatherInfo = await Singleton<WeatherService>.Instance.GetRefreshedWeatherAsync(WeatherInfo.City);
        }
    }
}
