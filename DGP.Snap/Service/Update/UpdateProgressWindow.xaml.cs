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

namespace DGP.Snap.Service.Update
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
        /// <summary>
        /// 以动画形式将进度条过渡到新的值
        /// </summary>
        /// <param name="value"></param>
        public void SetNewProgress(double value)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                To = value,
                Duration = new Duration(TimeSpan.FromMilliseconds(500))
            };
            ProgressBar.BeginAnimation(MetroProgressBar.ValueProperty, doubleAnimation);
        }
    }
}
