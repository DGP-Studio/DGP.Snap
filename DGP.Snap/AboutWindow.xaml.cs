using DGP.Snap.Helper.Extensions;
using System.Windows;

namespace DGP.Snap
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AboutWindow : System.Windows.Window
    {
        public AboutWindow()
        {

            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetAcrylicblur();
        }
    }
}
