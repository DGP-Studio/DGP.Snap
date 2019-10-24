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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DGP.Snap.Service.Shell
{
    /// <summary>
    /// TrayIconMenu.xaml 的交互逻辑
    /// </summary>
    public partial class TrayIconMenu : UserControl
    {
        public TrayIconMenu()
        {
            //InitializeComponent();
            
        }
        public object GetStyle(object resourceKey)
        {
             return this.FindResource(resourceKey);
        }
    }
}
