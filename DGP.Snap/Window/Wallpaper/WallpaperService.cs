using DGP.Snap.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Snap.Window.Wallpaper
{
    public static class WallPaperService
    {
        private const string WallPaper360BasedUrL = @"http://wallpaper.apc.360.cn/index.php?c=WallPaper&a=getAppsByOrder&order=create_time&start=0&count=20&from=360chrome";
        private const string WallPaperBingBasedUrL = @"https://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=8";

        public static List<WallpaperInfo> WallpaperInfos { get; private set; } = new List<WallpaperInfo>();
        /// <summary>
        /// 使用后在 <see cref="WallpaperInfos"/> 中获得壁纸信息
        /// </summary>
        /// <returns></returns>
        public static async Task Get360ImageUriCollectionAsync()
        {
            List<Uri> imageUriCollection = new List<Uri>();

            ThreeSixZeroWallpaperJsonObject wallPaper360JsonInfo = await Json.GetWebRequestJsonObjectAsync<ThreeSixZeroWallpaperJsonObject>(WallPaper360BasedUrL);//await GetRequest360WallPaperImageJsonInfoAsync();

            foreach (DataItemFor360 dataItem in wallPaper360JsonInfo.Data)
            {
                WallpaperInfo wallpaperInfo = new WallpaperInfo()
                {
                    ThumbnailUri = new Uri(dataItem.Url),
                    Description=dataItem.Utag,
                };
                WallpaperInfos.Add(wallpaperInfo);
            }
        }
        /// <summary>
        /// 使用后在 <see cref="WallpaperInfos"/> 中获得壁纸信息
        /// </summary>
        /// <returns></returns>
        public static async Task GetBingImageUriCollectionAsync()
        {
            string basedBingUrl = "http://cn.bing.com";

            BingImageJsonObject bingImageJsonInfo = await Json.GetWebRequestJsonObjectAsync<BingImageJsonObject>(WallPaperBingBasedUrL);

            foreach (ImageItemForBing imageItemForBing in bingImageJsonInfo.Images)
            {
                WallpaperInfo wallpaperInfo = new WallpaperInfo()
                {
                    ThumbnailUri= new Uri(basedBingUrl + imageItemForBing.Url),
                    Description=imageItemForBing.Copyright,
                };
                WallpaperInfos.Add(wallpaperInfo);
            }
        }
    }
}
