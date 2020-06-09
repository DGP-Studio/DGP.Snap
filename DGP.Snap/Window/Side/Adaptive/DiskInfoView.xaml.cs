using DGP.Snap.Service.Device;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace DGP.Snap.Window.Side.Adaptive
{
    /// <summary>
    /// DiskInfoView.xaml 的交互逻辑
    /// </summary>
    public partial class DiskInfoView : UserControl
    {
        public DiskInfoView()
        {
            InitializeComponent();
        }
        void GetDriveInfo()
        {
            DriveInfo[] allDirves = DriveInfo.GetDrives();
            //检索计算机上的所有逻辑驱动器名称
            foreach (DriveInfo item in allDirves)
            {
                if (item.IsReady)
                {
                    DiskInfos.Add(item);
                }
            }
        }

        public ObservableCollection<DriveInfo> DiskInfos = new ObservableCollection<DriveInfo>();
    }
}
