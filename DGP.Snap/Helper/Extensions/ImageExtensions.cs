using System;
using System.Drawing;
using System.Net;

namespace DGP.Snap.Helper.Extensions
{
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
