using DGP.Snap.Helper;
using DGP.Snap.Window.Weather;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DGP.Snap.Window.Side.Adaptive
{
    /// <summary>
    /// WeatherView.xaml 的交互逻辑
    /// </summary>
    public partial class WeatherView : UserControl
    {
        public WeatherView()
        {
            DataContext = this;
            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await Singleton<WeatherService>.Instance.InitializeAsync();
            WeatherInfo = Singleton<WeatherService>.Instance.WeatherInformation;
        }

        public WeatherModel WeatherInfo
        {
            get { return (WeatherModel)GetValue(WeatherInfoProperty); }
            set { SetValue(WeatherInfoProperty, value); }
        }
        public static readonly DependencyProperty WeatherInfoProperty =
            DependencyProperty.Register("WeatherInfo", typeof(WeatherModel), typeof(WeatherView));

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            WeatherInfo = await Singleton<WeatherService>.Instance.GetRefreshedWeatherAsync(WeatherInfo.City);
        }
        private async void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                WeatherInfo = await Singleton<WeatherService>.Instance.GetRefreshedWeatherAsync(((TextBox)sender).Text);
            }
        }
    }
}
