using DGP.Snap.Service.Navigation;
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
            Service.Navigation.NavigationService.Frame = currentFrame;
            Service.Navigation.NavigationService.Navigate<NotificationPage>();
            Service.Navigation.NavigationService.Navigated += Frame_Navigated;
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {

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
                Service.Navigation.NavigationService.Navigate<InformationPage>();
                return;
            }

            var item = NavigationView.Items
                            .OfType<HamburgerMenuItem>()
                            .First(menuItem => menuItem.Label == ((HamburgerMenuItem)args.InvokedItem).Label);

            var pageType = item.GetValue(NavHelper.NavigateToProperty) as Type;
            Service.Navigation.NavigationService.Navigate(pageType);
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
