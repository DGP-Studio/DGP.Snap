using DGP.Snap.Helper.Extensions;
using System;
using System.Windows;

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
        }

        //保持置底状态
        private void HandleWindowBottomState(object sender, EventArgs e) => this.SetBottomMost();

    }
}
