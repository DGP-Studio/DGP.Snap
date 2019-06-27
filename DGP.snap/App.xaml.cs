using DGP.Snap.Service.Shell;
using DGP.Snap.Service.Update;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace DGP.Snap
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {


        protected override async void OnStartup(StartupEventArgs e)
        {
            //托盘图标
            TrayIconManager.Instance();
            //更新
            await UpdateService.HandleUpdateCheck();

            base.OnStartup(e);

        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Debug.WriteLine(e.ExceptionObject.ToString());
        }

        protected override void OnExit(ExitEventArgs e)
        {
            TrayIconManager.Instance().Dispose();
            Debug.WriteLine("正常终止!");
            base.OnExit(e);
        }

    }
}
