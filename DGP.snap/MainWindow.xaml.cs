using DGP.Snap.Services.Navigation;
using MahApps.Metro.Controls;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace DGP.Snap
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Services.Navigation.NavigationService.Navigate<HomePage>();
            Services.Navigation.NavigationService.Navigated += Frame_Navigated;
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            throw new NotImplementedException();
        }

        public Frame CurrentFrame => currentFrame;

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState=WindowState.Minimized;
        }

        private void NavigationView_ItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs args)
        {
            if (args.IsItemOptions)
            {
                Services.Navigation.NavigationService.Navigate<SettingPage>();
                return;
            }

            var item = NavigationView.Items
                            .OfType<HamburgerMenuItem>()
                            .First(menuItem => menuItem.Label == ((HamburgerMenuItem)args.InvokedItem).Label);

            var pageType = item.GetValue(NavHelper.NavigateToProperty) as Type;
            Services.Navigation.NavigationService.Navigate(pageType);
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_StateChanged(object sender, EventArgs e)
        {

        }
    }
}
