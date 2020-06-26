using DGP.Snap.Helper.Extensions;
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
using System.Windows.Shapes;

namespace DGP.Snap.Service.Exception
{
    /// <summary>
    /// ExceptionDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ExceptionDialog : System.Windows.Window
    {
        public ExceptionDialog()
        {
            DataContext = this;
            InitializeComponent();
        }
        public ExceptionDialog(object sender, UnhandledExceptionEventArgs e)
        {
            DataContext = this;
            InitializeComponent();

            System.Exception ex = (System.Exception)e.ExceptionObject;
            ExceptionName = ex.GetType().Name;
            Message = ex.Message;
            TargetSite = ex.TargetSite.ToString();

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetAcrylicblur();
        }


        public string ExceptionName
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("ExceptionName", typeof(string), typeof(ExceptionDialog), new PropertyMetadata(""));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(ExceptionDialog), new PropertyMetadata(""));

        public string TargetSite
        {
            get { return (string)GetValue(TargetSiteProperty); }
            set { SetValue(TargetSiteProperty, value); }
        }
        public static readonly DependencyProperty TargetSiteProperty =
            DependencyProperty.Register("TargetSite", typeof(string), typeof(ExceptionDialog), new PropertyMetadata(""));

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(@"https://jinshuju.net/f/TZLthG");
        }
    }
}
