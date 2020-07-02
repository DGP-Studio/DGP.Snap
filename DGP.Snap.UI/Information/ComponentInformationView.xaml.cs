using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Navigation;

namespace DGP.Snap.UI.Information
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
        private void OnHyperLinkInvoked(object sender, RoutedEventArgs eventArgs)
        {
            Process.Start(((Hyperlink)sender).NavigateUri.ToString());
            eventArgs.Handled = true;
        }
        private void OnHyperlinkRequestNavigate(object sender, RequestNavigateEventArgs eventArgs)
        {
            eventArgs.Handled = true;
        }

        public ImageSource ComponentImageSource
        {
            get { return (ImageSource)GetValue(ComponentImageSourceProperty); }
            set { SetValue(ComponentImageSourceProperty, value); }
        }
        public static readonly DependencyProperty ComponentImageSourceProperty =
            DependencyProperty.Register("ComponentImageSource", typeof(ImageSource), typeof(ComponentInformationView));

        public string ComponentName
        {
            get { return (string)GetValue(ComponentNameProperty); }
            set { SetValue(ComponentNameProperty, value); }
        }
        public static readonly DependencyProperty ComponentNameProperty =
            DependencyProperty.Register("ComponentName", typeof(string), typeof(ComponentInformationView));

        public string ComponentAuthor
        {
            get { return (string)GetValue(ComponentAuthorProperty); }
            set { SetValue(ComponentAuthorProperty, value); }
        }
        public static readonly DependencyProperty ComponentAuthorProperty =
            DependencyProperty.Register("ComponentAuthor", typeof(string), typeof(ComponentInformationView));

        public string ComponentDescription
        {
            get { return (string)GetValue(ComponentDescriptionProperty); }
            set { SetValue(ComponentDescriptionProperty, value); }
        }
        public static readonly DependencyProperty ComponentDescriptionProperty =
            DependencyProperty.Register("ComponentDescription", typeof(string), typeof(ComponentInformationView));

        public Uri ComponentRepositoryUri
        {
            get { return (Uri)GetValue(ComponentRepositoryUriProperty); }
            set { SetValue(ComponentRepositoryUriProperty, value); }
        }
        public static readonly DependencyProperty ComponentRepositoryUriProperty =
            DependencyProperty.Register("ComponentRepositoryUri", typeof(Uri), typeof(ComponentInformationView));
    }
}
