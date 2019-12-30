using System;
using System.Diagnostics;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DGP.Snap.Updater
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private static void InstallPackage()
        {
            Thread.Sleep(2000);
            using (ZipArchive archive = ZipFile.OpenRead("Package.zip"))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    entry.ExtractToFile(entry.FullName, overwrite: true);
                }
            }
            Process.Start("DGP.Snap.exe");

            Application.Current.Shutdown();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => InstallPackage());
        }

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => DragMove();
    }
}
