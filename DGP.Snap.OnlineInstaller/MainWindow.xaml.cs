using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DGP.Snap.OnlineInstaller
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

        public static class Json
        {
            public static async Task<T> ToObjectAsync<T>(string value)
            {
                return await Task.Run(() =>
                {
                    return JsonConvert.DeserializeObject<T>(value);
                });
            }

            public static async Task<string> StringifyAsync(object value)
            {
                return await Task.Run(() =>
                {
                    JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
                    jsonSerializerSettings.NullValueHandling = NullValueHandling.Include;
                    jsonSerializerSettings.Formatting = Formatting.Indented;
                    //jsonSerializerSettings.
                    return JsonConvert.SerializeObject(value, jsonSerializerSettings);
                });
            }
        }
    }
}
