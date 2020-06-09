using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DGP.Snap.Window.Side.Counter
{
    /// <summary>
    /// CpuCounterView.xaml 的交互逻辑
    /// </summary>
    public partial class CpuCounterView : UserControl
    {
        PerformanceCounter cpuCounter;
        public CpuCounterView()
        {
            DataContext = this;
            InitializeComponent();

            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UsageRate = $"{(int)cpuCounter.NextValue()}%";
        }

        public string UsageRate
        {
            get { return (string)GetValue(UsageRateProperty); }
            set { SetValue(UsageRateProperty, value); }
        }
        public static readonly DependencyProperty UsageRateProperty =
            DependencyProperty.Register("UsageRate", typeof(string), typeof(CpuCounterView));
    }
}
