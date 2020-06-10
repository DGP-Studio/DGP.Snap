using MahApps.Metro.Controls;
using System;
using System.Windows;

namespace DGP.Snap.Service.Navigation
{
    public class NavHelper
    {
        public static Type GetNavigateTo(HamburgerMenuItem item)
        {
            return (Type)item.GetValue(NavigateToProperty);
        }

        public static void SetNavigateTo(HamburgerMenuItem item, Type value)
        {
            item.SetValue(NavigateToProperty, value);
        }

        public static readonly DependencyProperty NavigateToProperty =
            DependencyProperty.RegisterAttached("NavigateTo", typeof(Type), typeof(NavHelper), new PropertyMetadata(null));
    }
}
