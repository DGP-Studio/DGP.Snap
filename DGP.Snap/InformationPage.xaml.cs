using System.Windows;
using System.Windows.Controls;

namespace DGP.Snap
{
    /// <summary>
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class InformationPage : Page
    {
        public InformationPage()
        {
            AppVersion = Application.ResourceAssembly.GetName().Version.ToString();
            DataContext = this;
            InitializeComponent();
        }

        public string AppVersion
        {
            get { return (string)GetValue(AppVersionProperty); }
            set { SetValue(AppVersionProperty, value); }
        }
        public static readonly DependencyProperty AppVersionProperty =
            DependencyProperty.Register("AppVersion", typeof(string), typeof(InformationPage), new PropertyMetadata(""));

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SnapInfoView.ComponentDescription = $"v {AppVersion}";
        }
    }
}
