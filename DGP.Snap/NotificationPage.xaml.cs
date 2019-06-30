using DGP.Snap.UI.Notification;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DGP.Snap
{

    public partial class NotificationPage : Page
    {
        public NotificationPage()
        {
            InitializeComponent();
            NotificationPresenter.Children.Add(new Notification { Title="通知", Info="测试",IconSource= new BitmapImage(new System.Uri(@"/Resources/SnapNewLogo.png", System.UriKind.Relative)) });
        }
    }
}
