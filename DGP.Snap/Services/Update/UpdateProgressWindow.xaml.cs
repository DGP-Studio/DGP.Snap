using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DGP.Snap.Services.Update
{
    /// <summary>
    /// UpdateProgressWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateProgressWindow : System.Windows.Window
    {
        public UpdateProgressWindow()
        {
            InitializeComponent();
        }

        public void SetNewProgress(double value)
        {
            //实例化一个DoubleAnimation类。
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            //设置To属性。
            doubleAnimation.To = value;
            //设置Duration属性。
            doubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(500));
            //为元素设置BeginAnimation方法。
            ProgressBar.BeginAnimation(MetroProgressBar.ValueProperty, doubleAnimation);
        }
    }
}
