using DGP.Snap.Helper.Extensions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetBottomWithInteractivity();
            this.SetAcrylicblur();
        }

        public static event EventHandler OnRefreshRequired;

        private bool isFirstExtend = true;

        private void Window_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isFirstExtend)
            {
                isFirstExtend = false;
            }
            else
            {
                OnRefreshRequired.Invoke(this, new EventArgs());
            }

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

        private void SystemSettingButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("ms-settings:home");
        }
    }
}
