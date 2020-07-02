using DGP.Snap.Helper;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Snap.Service.User
{
    public class QQUserInfomationService : ISingleton
    {
        private const string BaseUriString = @"https://users.qzone.qq.com/fcg-bin/cgi_get_portrait.fcg?uins=";

        [EditorBrowsable(EditorBrowsableState.Never)] public QQUserInfomationService() { }

        public async Task<QQUserInfo> GetQQUserInfoAsync(string requestQQNumber)
        {
            string requestUrl = BaseUriString + requestQQNumber;
            HttpWebRequest request = WebRequest.CreateHttp(requestUrl);
            HttpWebResponse httpResp = (HttpWebResponse)request.GetResponse();
            using (Stream respStream = httpResp.GetResponseStream())
            {
                using (StreamReader respStreamReader = new StreamReader(respStream, Encoding.GetEncoding("GBK")))
                {
                    string responseString = respStreamReader.ReadToEnd();
                    int length = responseString.IndexOf("]") - responseString.IndexOf("[") + 1;
                    string arrayString = responseString.Substring(responseString.IndexOf("["), length);
                    string[] stringArray = await Json.ToObjectAsync<string[]>(arrayString);
                    return new QQUserInfo
                    {
                        QQUserAvatorUri = new Uri(stringArray[0]),
                        QQUserNickName = stringArray[6],
                    };
                }
            }
        }

        public void Initialize()
        {
            Debug.WriteLine(this);
        }
    }
}
