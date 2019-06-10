using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Snap.Updater.Helper
{
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
