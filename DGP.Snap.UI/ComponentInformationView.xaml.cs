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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DGP.Snap.UI
{
    /// <summary>
    /// ComponentInformationView.xaml 的交互逻辑
    /// </summary>
    public partial class ComponentInformationView : UserControl
    {
        public ComponentInformationView()
        {
            DataContext = this;
            InitializeComponent();
        }
        private void OnHyperLinkInvoked(object sender, RoutedEventArgs e)
        {
            Process.Start(((Hyperlink)sender).NavigateUri.ToString());
            e.Handled = true;
        }
        private void OnHyperlinkRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            e.Handled = true;
        }

        public ImageSource ComponentImageSource
        {
            get { return (ImageSource)GetValue(ComponentImageSourceProperty); }
            set { SetValue(ComponentImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ComponentImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComponentImageSourceProperty =
            DependencyProperty.Register("ComponentImageSource", typeof(ImageSource), typeof(ComponentInformationView));

        public string ComponentName
        {
            get { return (string)GetValue(ComponentNameProperty); }
            set { SetValue(ComponentNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ComponentName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComponentNameProperty =
            DependencyProperty.Register("ComponentName", typeof(string), typeof(ComponentInformationView));

        public string ComponentAuthor
        {
            get { return (string)GetValue(ComponentAuthorProperty); }
            set { SetValue(ComponentAuthorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ComponentAuthor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComponentAuthorProperty =
            DependencyProperty.Register("ComponentAuthor", typeof(string), typeof(ComponentInformationView));

        public string ComponentDescription
        {
            get { return (string)GetValue(ComponentDescriptionProperty); }
            set { SetValue(ComponentDescriptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ComponentDescription.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComponentDescriptionProperty =
            DependencyProperty.Register("ComponentDescription", typeof(string), typeof(ComponentInformationView));

        public Uri ComponentRepositoryUri
        {
            get { return (Uri)GetValue(ComponentRepositoryUriProperty); }
            set { SetValue(ComponentRepositoryUriProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ComponentRepositoryUri.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComponentRepositoryUriProperty =
            DependencyProperty.Register("ComponentRepositoryUri", typeof(Uri), typeof(ComponentInformationView));

        public Uri ComponentLicenseUri
        {
            get { return (Uri)GetValue(ComponentLicenseUriProperty); }
            set { SetValue(ComponentLicenseUriProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ComponentLicenseUri.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComponentLicenseUriProperty =
            DependencyProperty.Register("ComponentLicenseUri", typeof(Uri), typeof(ComponentInformationView));


    }
}
