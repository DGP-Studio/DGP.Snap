using MahApps.Metro.Controls;
using System;
using System.Management;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace DGP.Snap.Window.Side.Counter
{
    /// <summary>
    /// MemCounterView.xaml 的交互逻辑
    /// </summary>
    public partial class MemCounterView : UserControl
    {
        private double available = 0, capacity = 0;
        public MemCounterView()
        {
            DataContext = this;
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(5),
            };
            timer.Tick += Timer_Tick;
            timer.Start();

        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                //总
                using (ManagementClass totalMemoryManager = new ManagementClass("Win32_PhysicalMemory"))
                {
                    using (ManagementObjectCollection totalMemoryManagersObjects = totalMemoryManager.GetInstances())
                    {
                        foreach (ManagementObject manager1 in totalMemoryManagersObjects)
                        {
                            capacity += Math.Round(long.Parse(manager1.Properties["Capacity"].Value.ToString()) / 1024 / 1024 / 1024.0, 1);
                        }
                    }
                }
                //可用
                using (ManagementClass availableMemoryManager = new ManagementClass("Win32_PerfFormattedData_PerfOS_Memory"))
                {
                    using (ManagementObjectCollection availableMemoryManagerObjects = availableMemoryManager.GetInstances())
                    {
                        foreach (ManagementObject manager2 in availableMemoryManagerObjects)
                        {
                            available += ((Math.Round(long.Parse(manager2.Properties["AvailableMBytes"].Value.ToString()) / 1024.0, 1)));
                        }
                    }
                }
                this.Invoke(() => { UsedMem = $"{Math.Round((capacity - available) / capacity * 100, 0)}%"; });
            });
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
