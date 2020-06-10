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
                JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,
                    Formatting = Formatting.Indented
                };
                return JsonConvert.SerializeObject(value, jsonSerializerSettings);
            });
        }

        /// <summary>
        /// 向指定 <paramref name="requestUrl"/> 的服务器请求Json数据，并将结果返回为类型为 <typeparamref name="TRequestType"/> 的实例
        /// </summary>
        /// <typeparam name="TRequestType"></typeparam>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public static async Task<TRequestType> GetWebRequestJsonObjectAsync<TRequestType>(string requestUrl)
        {
            return await Task.Run(async () =>
            {
                string jsonMetaString = GetWebResponse(requestUrl);
                return await ToObjectAsync<TRequestType>(jsonMetaString);
            });
        }

        private static string GetWebResponse(string requestUrl)
        {
            HttpWebRequest request = WebRequest.CreateHttp(requestUrl);
            //为了能正常的获取GitHub的数据
            request.Proxy = WebRequest.DefaultWebProxy;
            request.Credentials = CredentialCache.DefaultCredentials;

            request.Method = "GET";
            request.ContentType = "application/json;charset=UTF-8";
            //request.Headers.Add(HttpRequestHeader.UserAgent, "Wget/1.9.1");
            request.UserAgent = "Wget/1.9.1";
            //request.Timeout = 5000;
            string jsonMetaString;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())//获取响应
            {
                using (StreamReader responseStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    jsonMetaString = responseStreamReader.ReadToEnd();
                }
            }

            return jsonMetaString;
        }


    }
}
