using DGP.Snap.Helper;
using DGP.Snap.Service.Setting;
using DGP.Snap.Service.Weather.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Ink;

namespace DGP.Snap.Service.Weather
{
    public class WeatherService : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }
            storage = value;
            OnPropertyChanged(propertyName);
        }
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

        public async Task<WeatherModel> GetRefreshedWeatherAsync(string cityname)
        {
            return await Xml.GetWebRequestObjectAsync<WeatherModel>(BaseUri + cityname);
        }

        public async Task InitializeAsync()
        {
            if (_weatherInformation == null)
            {
                object location = SettingService.GetInstance()["Weather_Location"];
                if(location!=null)
                    _weatherInformation = await Xml.GetWebRequestObjectAsync<WeatherModel>(BaseUri + (string)location);
                else
                    _weatherInformation = await Xml.GetWebRequestObjectAsync<WeatherModel>(BaseUri + "北京");
            }
            return;
        }

        private const string BaseUri = "http://wthrcdn.etouch.cn/WeatherApi?city=";
        private object weatherlock = new object();

        private WeatherModel _weatherInformation = null;
        public WeatherModel WeatherInformation
        {
            get
            {
                lock (weatherlock)
                {
                    return _weatherInformation;
                }
            }
            set
            {
                lock (weatherlock)
                {
                    Set(ref _weatherInformation, value);
                }
            }
        }

        #region 单例
        private static WeatherService instance;
        private static object _lock = new object();
        private WeatherService()
        {

        }
        public static WeatherService GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new WeatherService();
                    }
                }
            }
            return instance;
        }
        #endregion
    }
}
