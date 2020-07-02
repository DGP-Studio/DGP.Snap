using DGP.Snap.Service.Setting;
using DGP.Snap.Service.Weather;
using DGP.Snap.Service.Weather.Model;
using System.Diagnostics;
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
            SideWindow.OnRefreshRequired += OnRefreshRequiredAsync;
        }

        private async void UserControl_LoadedAsync(object sender, RoutedEventArgs e)
        {
            await WeatherService.GetInstance().InitializeAsync();
            WeatherInfo = WeatherService.GetInstance().WeatherInformation;
        }

        private async void OnRefreshRequiredAsync(object sender, System.EventArgs e)
        {
            WeatherInfo = await WeatherService.GetInstance().GetRefreshedWeatherAsync(WeatherInfo.City);
            Debug.WriteLine("weather");
        }

        public WeatherModel WeatherInfo
        {
            get { return (WeatherModel)GetValue(WeatherInfoProperty); }
            set { SetValue(WeatherInfoProperty, value); }
        }
        public static readonly DependencyProperty WeatherInfoProperty =
            DependencyProperty.Register("WeatherInfo", typeof(WeatherModel), typeof(WeatherView));

        private async void RefreshButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            WeatherInfo = await WeatherService.GetInstance().GetRefreshedWeatherAsync(WeatherInfo.City);
        }
        private async void TextBox_KeyDownAsync(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                WeatherInfo = await WeatherService.GetInstance().GetRefreshedWeatherAsync(((TextBox)sender).Text);
                SettingService.GetInstance().AppSettings["Weather_Location"] = ((TextBox)sender).Text;
                await SettingService.GetInstance().SaveSettingsAsync();
                ;
            }
        }
    }
}
