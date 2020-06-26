using DGP.Snap.Helper.Extensions;
using System;
using System.Windows;

namespace DGP.Snap
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetAcrylicblur();
        }

        private void Window_Deactivated(object sender, System.EventArgs e)
        {
            try
            {
                Close();
            }
            catch(Exception)
            {
            }
        }
    }
}
