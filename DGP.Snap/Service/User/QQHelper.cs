using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DGP.Snap.Service.User
{
    class QQHelper
    {
        public static string GetQQUserName(string requestQQNumber)
        {
            string qqUserName;
            string requestUrl = "https://users.qzone.qq.com/fcg-bin/cgi_get_portrait.fcg?uins=" + requestQQNumber;
            HttpWebRequest request = WebRequest.CreateHttp(requestUrl);
            HttpWebResponse httpResp = (HttpWebResponse)request.GetResponse();
            Stream respStream = httpResp.GetResponseStream();
            StreamReader respStreamReader = new StreamReader(respStream, Encoding.GetEncoding("GBK"));
            string strBuff = respStreamReader.ReadToEnd();

            string re1 = ".*?"; // Non-greedy match on filler
            string re2 = "\".*?\""; // Uninteresting: string
            string re3 = ".*?"; // Non-greedy match on filler
            string re4 = "\".*?\""; // Uninteresting: string
            string re5 = ".*?"; // Non-greedy match on filler
            string re6 = "(\".*?\")";   // Double Quote String 1
            Regex r = new Regex(re1 + re2 + re3 + re4 + re5 + re6, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(strBuff);
            if (m.Success)
            {
                qqUserName = m.Groups[1].ToString();
                qqUserName = qqUserName.Replace("\"", "");
                return qqUserName;
            }
            return null;
        }
    }

    class GetQQInfo
    {

        public static ImageBrush Getqq_avatar(String qqnumber)
        {
            Image image = ImageExtensions.GetImageFromNet("http://q1.qlogo.cn/g?b=qq&nk=" + qqnumber + "&s=5");
            ImageBrush qq_avatar;
            if (image != null)
            {
                var bitmap = new Bitmap(image);
                var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap
                    (bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                bitmap.Dispose();
                var brush = new ImageBrush(bitmapSource);
                qq_avatar = brush;
            }
            else
            {
                qq_avatar = null;
            }
            return qq_avatar;
        }

        public static String Getqq_name(String qqnumber)
        {
            String qq_name;
            String request_name = "https://users.qzone.qq.com/fcg-bin/cgi_get_portrait.fcg?uins=" + qqnumber;
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(request_name);
            try//如果无网络
            {
                HttpWebResponse httpResp = (HttpWebResponse)httpReq.GetResponse();
                Stream respStream = httpResp.GetResponseStream();
                StreamReader respStreamReader = new StreamReader(respStream, Encoding.GetEncoding("GBK"));
                String strBuff = respStreamReader.ReadToEnd();
                string re1 = ".*?"; // Non-greedy match on filler
                string re2 = "\".*?\""; // Uninteresting: string
                string re3 = ".*?"; // Non-greedy match on filler
                string re4 = "\".*?\""; // Uninteresting: string
                string re5 = ".*?"; // Non-greedy match on filler
                string re6 = "(\".*?\")";   // Double Quote String 1
                Regex r = new Regex(re1 + re2 + re3 + re4 + re5 + re6, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                Match m = r.Match(strBuff);
                if (m.Success)
                {
                    qq_name = m.Groups[1].ToString();
                    qq_name = qq_name.Replace("\"", "");
                    return qq_name;
                }
            }
            catch//则字符串为空
            {

            }
            return "";
        }
    }

    public static class ImageExtensions
    {
        public static Image GetImageFromNet(this string url, Action<WebRequest> requestAction = null, Func<WebResponse, Image> responseFunc = null)
        {
            return new Uri(url).GetImageFromNet(requestAction, responseFunc);
        }

        public static Image GetImageFromNet(this Uri url, Action<WebRequest> requestAction = null, Func<WebResponse, Image> responseFunc = null)
        {
            Image img;
            try
            {
                WebRequest request = WebRequest.Create(url);
                requestAction?.Invoke(request);
                using (WebResponse response = request.GetResponse())
                {
                    if (responseFunc != null)
                    {
                        img = responseFunc(response);
                    }
                    else
                    {
                        img = Image.FromStream(response.GetResponseStream());
                    }
                }
            }
            catch
            {
                img = null;
            }
            return img;
        }
    }
}
