using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DGP.Snap.Window.FrontSight
{
    /// <summary>
    /// FrontSightWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FrontSightWindow : System.Windows.Window
    {
        public FrontSightWindow()
        {
            InitializeComponent();
            WindowManager.IsFrontSightWindowShowing = true;
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            WindowManager.IsFrontSightWindowShowing = false;
            base.OnClosing(e);
        }

        private void Left_Click(object sender, RoutedEventArgs e)
        {
            Left -= 1;
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            Left += 1;
        }

        private void Top_Click(object sender, RoutedEventArgs e)
        {
            Top -= 1;
        }

        private void Bottom_Click(object sender, RoutedEventArgs e)
        {
            Top += 1;
        }
    }
}
