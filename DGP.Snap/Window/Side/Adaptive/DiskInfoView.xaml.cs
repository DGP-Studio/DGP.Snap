using DGP.Snap.Service.Device;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace DGP.Snap.Window.Side.Adaptive
{
    /// <summary>
    /// DiskInfoView.xaml 的交互逻辑
    /// </summary>
    public partial class DiskInfoView : UserControl
    {
        public DiskInfoView()
        {
            DiskInfos = new ObservableCollection<DiskInfo>();

            DriveInfo[] allDirves = DriveInfo.GetDrives();
            foreach (DriveInfo item in allDirves)
            {
                if (item.IsReady)
                {
                    DiskInfos.Add(item);
                }
            }

            DataContext = this;
            InitializeComponent();
            SideWindow.OnRefreshRequired += OnRefreshRequired;
        }

        private void OnRefreshRequired(object sender, EventArgs e)
        {
            DiskInfos.Clear();
            DriveInfo[] allDirves = DriveInfo.GetDrives();
            foreach (DriveInfo item in allDirves)
            {
                if (item.IsReady)
                {
                    DiskInfos.Add(item);
                }
            }
            Debug.WriteLine("disk");
        }

        public ObservableCollection<DiskInfo> DiskInfos
        {
            get { return (ObservableCollection<DiskInfo>)GetValue(DiskInfosProperty); }
            set { SetValue(DiskInfosProperty, value); }
        }
        public static readonly DependencyProperty DiskInfosProperty =
            DependencyProperty.Register("DiskInfos", typeof(ObservableCollection<DiskInfo>), typeof(DiskInfoView));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", ((Button)sender).Tag.ToString() + "\\");
        }
    }
}
