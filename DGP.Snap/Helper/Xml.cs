using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DGP.Snap.Helper
{
    internal class Xml
    {
        /// <summary>
        /// 向指定 <paramref name="requestUrl"/> 的服务器请求Xml数据，并将结果返回为类型为 <typeparamref name="TRequestType"/> 的实例
        /// </summary>
        /// <typeparam name="TRequestType">请求的类类型</typeparam>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public static async Task<TRequestType> GetWebRequestObjectAsync<TRequestType>(string requestUrl)
        {
            HttpWebRequest request = WebRequest.CreateHttp(requestUrl);
            //request.Proxy = WebRequest.DefaultWebProxy;
            //request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "GET";
            //request.UserAgent = "Wget/1.9.1";
            request.Headers.Add("Accept-Encoding", "gzip,deflate");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            string xmlMetaString;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())//获取响应
            {
                using (StreamReader responseStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    xmlMetaString = responseStreamReader.ReadToEnd();
                }
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlMetaString);

            using (StringWriter stringWriter = new StringWriter())
            {
                using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
                {
                    xmlTextWriter.Indentation = 2;  // the Indentation
                    xmlTextWriter.Formatting = Formatting.Indented;
                    //上面两行可以不用
                    xmlDocument.WriteContentTo(xmlTextWriter);
                }
                xmlMetaString = stringWriter.ToString();
            }
            MemoryStream xmlstream = new MemoryStream(buffer: Encoding.UTF8.GetBytes(xmlMetaString));

            //前面的转换是因为serializer不能读取压缩的流
            return await Task.Run(() =>
            {
                return (TRequestType)new XmlSerializer(typeof(TRequestType)).Deserialize(xmlstream);
            });
        }

    }
}
