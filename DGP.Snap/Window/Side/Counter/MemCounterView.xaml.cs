using DGP.Snap.Service.Kernel;
using MahApps.Metro.Controls;
using System;
using System.Management;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using static DGP.Snap.Service.Kernel.NativeMethod;

namespace DGP.Snap.Window.Side.Counter
{
    /// <summary>
    /// MemCounterView.xaml 的交互逻辑
    /// </summary>
    public partial class MemCounterView : UserControl
    {
        public MemCounterView()
        {
            DataContext = this;
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1),
            };
            timer.Tick += Timer_Tick;
            timer.Start();

        }

        private void Timer_Tick(object sender, EventArgs eventArgs)
        {
            MemoryStatusEx memoryInfo = new MemoryStatusEx();
            memoryInfo.dwLength= (uint)Marshal.SizeOf(memoryInfo);
            GlobalMemoryStatusEx(ref memoryInfo);
            UsedMem = $"{Math.Round((memoryInfo.ullTotalPhys - memoryInfo.ullAvailPhys) / (double)memoryInfo.ullTotalPhys * 100, 0)}%";
        }

        public string UsedMem
        {
            get { return (string)GetValue(UsedMemProperty); }
            set { SetValue(UsedMemProperty, value); }
        }
        public static readonly DependencyProperty UsedMemProperty =
            DependencyProperty.Register("UsedMem", typeof(string), typeof(MemCounterView));

        public void Stop()
        {

        }

    }
}
