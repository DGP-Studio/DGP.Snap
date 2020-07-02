using DGP.Snap.Helper.Extensions;
using DGP.Snap.Service.Setting;
using DGP.Snap.Service.Shell;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
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

            Left = SystemParameters.WorkArea.Width - 72;
            Top = 0;
            Width = 420;
            Height = SystemParameters.WorkArea.Height;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetBottomWithInteractivity();
            this.SetAcrylicblur();
        }
        /// <summary>
        /// 不使用EventArgs
        /// </summary>
        public static event EventHandler OnRefreshRequired;

        private bool isFirstExtend = true;

        private async void Window_MouseEnterAsync(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isFirstExtend)
            {
                isFirstExtend = false;
            }
            else
            {
                await Task.Run(() =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        OnRefreshRequired.Invoke(this, new EventArgs());
                    });
                });

            }
            ExtendSideBar();
        }

        private void Window_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RetractSideBar();
        }

        private void ExtendSideBar()
        {
            double to = SystemParameters.WorkArea.Width - 416;
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
            double to = SystemParameters.WorkArea.Width - 72;
            DoubleAnimation posAnimation = new DoubleAnimation()
            {
                To = to,
                Duration = TimeSpan.FromSeconds(0.4),
                EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseInOut },
            };
            BeginAnimation(LeftProperty, posAnimation);
        }

        private void SystemSettingButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("ms-settings:home");
        }

        private void ThemeButton_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ToggleTheme();
            SettingService.GetInstance()["App_Theme"] = ThemeManager.GetCurrentTheme();
        }
    }
}
