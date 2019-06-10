using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Snap.Helper
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
        public static async Task<TRequestType> GetWebRequestJsonObject<TRequestType>(string requesturl)
        {
            //WebRequest webRequest = WebRequest.CreateHttp
            

            
            HttpWebRequest request = (HttpWebRequest)WebRequest.CreateHttp(requesturl);
            request.Proxy = WebRequest.DefaultWebProxy;
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "GET";
            request.ContentType = "application/json;charset=UTF-8";
            //request.Headers.Add(HttpRequestHeader.UserAgent, "Wget/1.9.1");
            request.UserAgent = "Wget/1.9.1";
            string jsonMetaString;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())//获取响应
            {
                using (StreamReader responseStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    jsonMetaString = responseStreamReader.ReadToEnd();
                }
            }
            return await ToObjectAsync<TRequestType>(jsonMetaString);
        }
    }
}
