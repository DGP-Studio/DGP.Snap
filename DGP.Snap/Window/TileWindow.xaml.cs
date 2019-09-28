using DGP.Snap.Helper.Extensions;
using System;
using System.Windows;

namespace DGP.Snap.Window
{
    /// <summary>
    /// 此窗口保持置于Windows底层的状态，向其中加入继承<see cref="UIElement"/>的实例以实现置底
    /// 目前尚未实现多窗口支持
    /// </summary>
    public partial class TileWindow : System.Windows.Window
    {
        public TileWindow()
        {
            InitializeComponent();

            Height = SystemParameters.PrimaryScreenHeight;
            Width = SystemParameters.PrimaryScreenWidth;

            Left = 0;
            Top = 0;
        }

        //保持置底状态
        private void HandleWindowBottomState(object sender, EventArgs e) => this.SetBottomMost();

    }
}
