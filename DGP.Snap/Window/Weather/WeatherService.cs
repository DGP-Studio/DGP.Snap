using DGP.Snap.Helper;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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


        public async Task InitializeAsync()
        {
            if (_weatherInformation == null)
                _weatherInformation = await Xml.GetWebRequestXmlObjectAsync<WeatherModel>(BaseUri + "余姚");
        }

        private const string BaseUri = "http://wthrcdn.etouch.cn/WeatherApi?city=";

        private WeatherModel _weatherInformation = null;
        public WeatherModel WeatherInformation
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
