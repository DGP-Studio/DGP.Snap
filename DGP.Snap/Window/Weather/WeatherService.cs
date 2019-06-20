using DGP.Snap.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Snap.Window.Weather
{
    public class WeatherService : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }
            storage = value;
            OnPropertyChanged(propertyName);
        }
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        private const string BaseUri = "http://wthrcdn.etouch.cn/WeatherApi?city=";
        public async Task<WeatherQueryModel> GetWeatherAsync()
        {
            return await Xml.GetWebRequestXmlObjectAsync<WeatherQueryModel>(BaseUri+"余姚");
        }

        private WeatherQueryModel _weatherInformation = null;
        public WeatherQueryModel WeatherInformation
        {
            get
            {
                return _weatherInformation;
            }
            set
            {
                Set(ref _weatherInformation, value);
            }
        }
    }
}
