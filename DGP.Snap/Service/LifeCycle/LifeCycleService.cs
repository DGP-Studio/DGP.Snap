using DGP.Snap.Service.Setting;
using System.Threading.Tasks;

namespace DGP.Snap.Service.LifeCycle
{
    internal class LifeCycleService
    {
        public static async void Application_ExitAsync()
        {
            await Task.Run(async () =>
            await SettingService.GetInstance().SaveSettingsAsync())
            .ContinueWith(async (t) =>
            await Task.Run(() =>
            App.Current.Dispatcher.Invoke(() =>
            App.Current.Shutdown())));
        }
    }
}
