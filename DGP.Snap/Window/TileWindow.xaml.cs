using DGP.Snap.Helper.Extensions;
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
using System.Windows.Shapes;

namespace DGP.Snap.Window
{
    /// <summary>
    /// TileWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TileWindow : System.Windows.Window
    {
        public TileWindow()
        {
            InitializeComponent();

            Height = SystemParameters.PrimaryScreenHeight;
            Width = SystemParameters.PrimaryScreenWidth;

            Left = Top = 0;
            this.SetBottomMost();
        }

        //保持置底状态
        private void HandleWindowBottomState(object sender, EventArgs e) => this.SetBottomMost();

    }
}
