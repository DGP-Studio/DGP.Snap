using DGP.Snap.Helper;
using DGP.Snap.Helper.Extensions;
using DGP.Snap.Window.Weather;
using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace DGP.Snap.Window.Side
{
    /// <summary>
    /// SideWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SideWindow : System.Windows.Window
    {
        public SideWindow()
        {
            DataContext = this;
            InitializeComponent();

            Left = Screen.PrimaryScreen.Bounds.Width - 72;
            Top = 0;
            Width = 420;
            Height = Screen.PrimaryScreen.WorkingArea.Height;  
        }

        private async void Window_LoadedAsync(object sender, RoutedEventArgs e)
        {
            //Weather
            await Singleton<WeatherService>.Instance.InitializeAsync();
            WeatherInfo = Singleton<WeatherService>.Instance.WeatherInformation;

            this.SetBottomWithInteractivity();
            this.SetAcrylicblur();
        }

        private void Window_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ExtendSideBar();
        }
        private void Window_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RetractSideBar();
        }

        private void ExtendSideBar()
        {
            double to = Screen.PrimaryScreen.WorkingArea.Width - 416;
            DoubleAnimation posAnimation = new DoubleAnimation()
            {
                To = to,
                Duration = TimeSpan.FromSeconds(0.4),
                EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseInOut },
            };
            BeginAnimation(LeftProperty, posAnimation);
        }

        private void RetractSideBar()
        {
            double to = Screen.PrimaryScreen.WorkingArea.Width - 72;
            DoubleAnimation posAnimation = new DoubleAnimation()
            {
                To = to,
                Duration = TimeSpan.FromSeconds(0.4),
                EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseInOut },
            };
            BeginAnimation(LeftProperty, posAnimation);
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            this.SetBottomWithInteractivity();
        }

        #region Weather
        public WeatherModel WeatherInfo
        {
            get { return (WeatherModel)GetValue(WeatherInfoProperty); }
            set { SetValue(WeatherInfoProperty, value); }
        }
        public static readonly DependencyProperty WeatherInfoProperty =
            DependencyProperty.Register("WeatherInfo", typeof(WeatherModel), typeof(SideWindow));

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            WeatherInfo = await Singleton<WeatherService>.Instance.GetRefreshedWeatherAsync(WeatherInfo.City);
        }
        private async void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                WeatherInfo = await Singleton<WeatherService>.Instance.GetRefreshedWeatherAsync(((System.Windows.Controls.TextBox)sender).Text);
            }
        }
        #endregion


    }
}
