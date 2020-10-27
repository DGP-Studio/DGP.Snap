using DGP.Snap.Helper;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;

namespace DGP.Snap.Window.Wallpaper
{
    /// <summary>
    /// 使用时先调用<see cref="InitializeAsync()"/>
    /// </summary>
    public class WallpaperService
    {
        /// <summary>
        /// 异步初始化 <see cref="WallpaperService"/>
        /// </summary>
        public async void InitializeAsync()
        {
            if (WallpaperInfos.Count < 1)
            {
                //网络问题
                try
                {
                    await GetBingImageUriCollectionAsync();
                    await Get360ImageUriCollectionAsync();
                }
                catch (WebException)
                {

                }
            }
        }

        public event EventHandler<bool> InitializeCompleted;

        private const string WallPaper360BaseUrL = @"http://wallpaper.apc.360.cn/index.php?c=WallPaper&a=getAppsByOrder&order=create_time&start=0&count=180&from=360chrome";
        private const string WallPaperBingBaseUrL = @"https://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=8";

        public ObservableCollection<Wallpaper> WallpaperInfos { get; private set; } = new ObservableCollection<Wallpaper>();

        /// <summary>
        /// 使用后在 <see cref="WallpaperInfos"/> 中获得壁纸信息
        /// </summary>
        /// <returns></returns>
        private async Task Get360ImageUriCollectionAsync()
        {

            BirdWallpaperJsonObject wallPaper360JsonInfo = await Json.GetWebRequestObjectAsync<BirdWallpaperJsonObject>(WallPaper360BaseUrL);//await GetRequest360WallPaperImageJsonInfoAsync();

            foreach (DataItemFor360 dataItem in wallPaper360JsonInfo.Data)
            {
                Wallpaper wallpaperInfo = new Wallpaper()
                {
                    Uri = new Uri(dataItem.Url),
                    Description = dataItem.Utag,
                };
                WallpaperInfos.Add(wallpaperInfo);
            }
        }

        /// <summary>
        /// 使用后在 <see cref="WallpaperInfos"/> 中获得壁纸信息
        /// </summary>
        /// <returns></returns>
        private async Task GetBingImageUriCollectionAsync()
        {
            string basedBingUrl = "http://cn.bing.com";

            BingImageJsonObject bingImageJsonInfo = await Json.GetWebRequestObjectAsync<BingImageJsonObject>(WallPaperBingBaseUrL);

            foreach (ImageItemForBing imageItemForBing in bingImageJsonInfo.Images)
            {
                Wallpaper wallpaperInfo = new Wallpaper()
                {
                    Uri = new Uri(basedBingUrl + imageItemForBing.Url),
                    Description = imageItemForBing.Copyright,
                };
                WallpaperInfos.Add(wallpaperInfo);
            }
        }
        #region 单例
        private static WallpaperService instance;
        private static object _lock = new object();
        private WallpaperService()
        {

        }
        public static WallpaperService GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new WallpaperService();
                    }
                }
            }
            return instance;
        }
        #endregion
    }
}
