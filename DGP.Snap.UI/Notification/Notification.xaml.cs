using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DGP.Snap.UI.Notification
{
    /// <summary>
    /// Notification.xaml 的交互逻辑
    /// </summary>
    public partial class Notification : UserControl
    {
        public RoutedEventHandler PrimaryButtonClickEventHandler { get; set; } = null;
        public RoutedEventHandler SecondaryButtonClickEventHandler { get; set; } = null;
        public RoutedEventHandler CloseButtonClickEventHander { get; set; } = null;

        public Notification()
        {
            DataContext = this;
            InitializeComponent();
        }

        public Notification(string title, string info) : this()
        {
            Title = title;
            Info = info;
        }


        private void PrimaryButton_Click(object sender, RoutedEventArgs e)
        {

            PrimaryButtonClickEventHandler?.Invoke(sender, e);
            ((StackPanel)Parent).Children.Remove(this);
        }

        private void SecondaryButton_Click(object sender, RoutedEventArgs e)
        {
            SecondaryButtonClickEventHandler?.Invoke(sender, e);
            ((StackPanel)Parent).Children.Remove(this);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseButtonClickEventHander?.Invoke(sender, e);
            ((StackPanel)Parent).Children.Remove(this);
        }

        ////////////////////////////////////////////////////////

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(Notification));

        public string Info
        {
            get { return (string)GetValue(InfoProperty); }
            set { SetValue(InfoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Info.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InfoProperty =
            DependencyProperty.Register("Info", typeof(string), typeof(Notification));

        public ImageSource IconSource
        {
            get { return (ImageSource)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.Register("IconSource", typeof(ImageSource), typeof(Notification));

        public Visibility PrimaryButtonVisibility
        {
            get { return (Visibility)GetValue(PrimaryButtonVisibilityProperty); }
            set { SetValue(PrimaryButtonVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PrimaryButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PrimaryButtonVisibilityProperty =
            DependencyProperty.Register("PrimaryButtonVisibility", typeof(Visibility), typeof(Notification), new PropertyMetadata(Visibility.Visible));

        public Visibility SecondaryButtonVisibility
        {
            get { return (Visibility)GetValue(SecondaryButtonVisibilityProperty); }
            set { SetValue(SecondaryButtonVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SecondaryButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SecondaryButtonVisibilityProperty =
            DependencyProperty.Register("SecondaryButtonVisibility", typeof(Visibility), typeof(Notification), new PropertyMetadata(Visibility.Visible));

        public Visibility CloseButtonVisibility
        {
            get { return (Visibility)GetValue(CloseButtonVisibilityProperty); }
            set { SetValue(CloseButtonVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseButtonVisibilityProperty =
            DependencyProperty.Register("CloseButtonVisibility", typeof(Visibility), typeof(Notification), new PropertyMetadata(Visibility.Visible));

    }
}
