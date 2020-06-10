using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace DGP.Snap.Window.Side.Counter
{
    /// <summary>
    /// WebCounterView.xaml 的交互逻辑
    /// </summary>
    public partial class WebCounterView : UserControl
    {
        private List<PerformanceCounter> dataSentCounters = new List<PerformanceCounter>();
        private List<PerformanceCounter> dataReceivedCounters = new List<PerformanceCounter>();

        public WebCounterView()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            float up = 0.0f, down = 0.0f;
            foreach (PerformanceCounter sendCounter in dataSentCounters)
            {
                up += sendCounter.NextValue();
            }
            foreach (PerformanceCounter receiveCount in dataReceivedCounters)
            {
                down += receiveCount.NextValue();
            }

            Upload = $"{up / 1024:#0.0}KB";
            Download = $"{down / 1024:#0.0}KB";
        }
        //public double GetNetworkUtilization(string networkCard)
        //{

        //    const int numberOfIterations = 10;



        //    float sendSum = 0;
        //    float receiveSum = 0;

        //    for (int index = 0; index < numberOfIterations; index++)
        //    {
        //        sendSum += dataSentCounter.NextValue();
        //        receiveSum += dataReceivedCounter.NextValue();
        //    }
        //    float dataSent = sendSum;
        //    float dataReceived = receiveSum;


        //    double utilization = (8 * (dataSent + dataReceived)) / (bandwidth * numberOfIterations) * 100;
        //    return utilization;
        //}

        //public string[] GetNetworkCards()
        //{
        //    return new PerformanceCounterCategory("Network Interface").GetInstanceNames();
        //}
        public string Upload
        {
            get { return (string)GetValue(UploadProperty); }
            set { SetValue(UploadProperty, value); }
        }
        public static readonly DependencyProperty UploadProperty =
            DependencyProperty.Register("Upload", typeof(string), typeof(WebCounterView));

        public string Download
        {
            get { return (string)GetValue(DownloadProperty); }
            set { SetValue(DownloadProperty, value); }
        }
        public static readonly DependencyProperty DownloadProperty =
            DependencyProperty.Register("Download", typeof(string), typeof(WebCounterView));

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string networkCard in new PerformanceCounterCategory("Network Interface").GetInstanceNames())
            {
                dataSentCounters.Add(new PerformanceCounter("Network Interface", "Bytes Sent/sec", networkCard));
                dataReceivedCounters.Add(new PerformanceCounter("Network Interface", "Bytes Received/sec", networkCard));
            }

            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }
    }
}
