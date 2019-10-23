using DGP.Snap.Helper;
using DGP.Snap.Service.Shell;
using DGP.Snap.Service.Update;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;

namespace DGP.Snap
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {


        protected override void OnStartup(StartupEventArgs e)
        {
            if(!Debugger.IsAttached)//不调试时
                AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            base.OnStartup(e);
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            TrayIconManager.SystemNotificationManager.ShowNotification("Snap Deskyop/Exception", e.ExceptionObject.ToString()); 
            Debug.WriteLine(e.ExceptionObject.ToString());
        }

        private bool _isFirstInstance;
        private Mutex _isRunning;

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
#if DEBUG
            if (Debugger.IsAttached)//调试模式
                _isRunning = new Mutex(true, "DGP.Snap.Mutex.Debug.Debuging", out _isFirstInstance);
            else
                _isRunning = new Mutex(true, "DGP.Snap.Mutex.Debug", out _isFirstInstance);

            if (!_isFirstInstance)
            {
                Shutdown();
                return;
            }
#else
            _isRunning = new Mutex(true, "DGP.Snap.Mutex.Release", out _isFirstInstance);

            if (!_isFirstInstance)
            {
                Shutdown();
                return;
            }
#endif
            //托盘图标
            TrayIconManager.Instance();
            //更新
            await Singleton<UpdateService>.Instance.HandleUpdateCheck();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (!_isFirstInstance)
                return;
            _isRunning.ReleaseMutex();

            TrayIconManager.Instance().Dispose();
            //Debug.WriteLine("正常终止!");
            base.OnExit(e);
        }
    }
}
