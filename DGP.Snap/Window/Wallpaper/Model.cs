using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DGP.Snap.Window.Wallpaper
{
    /// <summary>
    /// 定义壁纸的共有基本信息与操作类型
    /// </summary>
    public class Wallpaper
    {
        public Uri Uri { get; set; }
        public string Description { get; set; }
    }

    public class ThreeSixZeroWallpaperJsonObject
    {
        [JsonProperty("errno")] public string Errno { get; set; }
        [JsonProperty("errmsg")] public string Errmsg { get; set; }
        [JsonProperty("consume")] public string Consume { get; set; }
        [JsonProperty("total")] public string Total { get; set; }
        [JsonProperty("data")] public List<DataItemFor360> Data { get; set; }
    }

    public class DataItemFor360
    {
        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("class_id")] public string Class_id { get; set; }
        [JsonProperty("resolution")] public string Resolution { get; set; }
        [JsonProperty("url_mobile")] public string Url_mobile { get; set; }
        [JsonProperty("url")] public string Url { get; set; }
        [JsonProperty("url_thumb")] public string Url_thumb { get; set; }
        [JsonProperty("url_mid")] public string Url_mid { get; set; }
        [JsonProperty("download_times")] public string Download_times { get; set; }
        [JsonProperty("imgcut")] public string Imgcut { get; set; }
        private string tag;
        [JsonProperty("tag")] public string Tag { get { return tag; } set { tag = value.Replace("_", "").Replace("category", ""); } }
        [JsonProperty("create_time")] public string Create_time { get; set; }
        [JsonProperty("update_time")] public string Update_time { get; set; }
        [JsonProperty("ad_id")] public string Ad_id { get; set; }
        [JsonProperty("ad_img")] public string Ad_img { get; set; }
        [JsonProperty("ad_pos")] public string Ad_pos { get; set; }
        [JsonProperty("ad_url")] public string Ad_url { get; set; }
        [JsonProperty("ext_1")] public string Ext_1 { get; set; }
        [JsonProperty("ext_2")] public string Ext_2 { get; set; }
        [JsonProperty("utag")] public string Utag { get; set; }
        [JsonProperty("tempdata")] public string Tempdata { get; set; }
        [JsonProperty("rdata")] public List<string> Rdata { get; set; }
        [JsonProperty("img_1600_900")] public string Img_1600_900 { get; set; }
        [JsonProperty("img_1440_900")] public string Img_1440_900 { get; set; }
        [JsonProperty("img_1366_768")] public string Img_1366_768 { get; set; }
        [JsonProperty("img_1280_800")] public string Img_1280_800 { get; set; }
        [JsonProperty("img_1280_1024")] public string Img_1280_1024 { get; set; }
        [JsonProperty("img_1024_768")] public string Img_1024_768 { get; set; }
    }

    public class ImageItemForBing
    {
        [JsonProperty("startdate")] public string Startdate { get; set; }
        [JsonProperty("fullstartdate")] public string Fullstartdate { get; set; }
        [JsonProperty("enddate")] public string Enddate { get; set; }
        [JsonProperty("url")] public string Url { get; set; }
        [JsonProperty("urlbase")] public string Urlbase { get; set; }
        [JsonProperty("copyright")] public string Copyright { get; set; }
        [JsonProperty("copyrightlink")] public string Copyrightlink { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("quiz")] public string Quiz { get; set; }
        [JsonProperty("wp")] public string Wp { get; set; }
        [JsonProperty("hsh")] public string Hsh { get; set; }
        [JsonProperty("drk")] public int Drk { get; set; }
        [JsonProperty("top")] public int Top { get; set; }
        [JsonProperty("bot")] public int Bot { get; set; }
        [JsonProperty("hs")] public List<string> Hs { get; set; }
    }

    public class TooltipsForBing
    {
        [JsonProperty("loading")] public string Loading { get; set; }
        [JsonProperty("previous")] public string Previous { get; set; }
        [JsonProperty("next")] public string Next { get; set; }
        [JsonProperty("walle")] public string Walle { get; set; }
        [JsonProperty("walls")] public string Walls { get; set; }
    }

    public class BingImageJsonObject
    {
        [JsonProperty("images")] public List<ImageItemForBing> Images { get; set; }
        [JsonProperty("tooltips")] public TooltipsForBing Tooltips { get; set; }
    }

    public class OwnerForBaidu
    {
        [JsonProperty("userName")] public string UserName { get; set; }
        [JsonProperty("userId")] public string UserId { get; set; }
        [JsonProperty("userSign")] public string UserSign { get; set; }
        [JsonProperty("isSelf")] public string IsSelf { get; set; }
        [JsonProperty("portrait")] public string Portrait { get; set; }
        [JsonProperty("isVip")] public string IsVip { get; set; }
        [JsonProperty("isLanv")] public string IsLanv { get; set; }
        [JsonProperty("isJiaju")] public string IsJiaju { get; set; }
        [JsonProperty("isHunjia")] public string IsHunjia { get; set; }
        [JsonProperty("orgName")] public string OrgName { get; set; }
        [JsonProperty("resUrl")] public string ResUrl { get; set; }
        [JsonProperty("cert")] public string Cert { get; set; }
        [JsonProperty("budgetNum")] public string BudgetNum { get; set; }
        [JsonProperty("lanvName")] public string LanvName { get; set; }
        [JsonProperty("contactName")] public string contactName { get; set; }
    }

    public class ImgsItemForBaidu
    {
        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("desc")] public string Desc { get; set; }
        [JsonProperty("tags")] public List<string> Tags { get; set; }
        [JsonProperty("owner")] public OwnerForBaidu Owner { get; set; }
        [JsonProperty("fromPageTitle")] public string FromPageTitle { get; set; }
        [JsonProperty("column")] public string Column { get; set; }
        [JsonProperty("parentTag")] public string ParentTag { get; set; }
        [JsonProperty("date")] public string Date { get; set; }
        [JsonProperty("downloadUrl")] public string DownloadUrl { get; set; }
        [JsonProperty("imageUrl")] public string ImageUrl { get; set; }
        [JsonProperty("imageWidth")] public int ImageWidth { get; set; }
        [JsonProperty("imageHeight")] public int ImageHeight { get; set; }
        [JsonProperty("thumbnailUrl")] public string ThumbnailUrl { get; set; }
        [JsonProperty("thumbnailWidth")] public int ThumbnailWidth { get; set; }
        [JsonProperty("thumbnailHeight")] public int ThumbnailHeight { get; set; }
        [JsonProperty("thumbLargeWidth")] public int ThumbLargeWidth { get; set; }
        [JsonProperty("thumbLargeHeight")] public int ThumbLargeHeight { get; set; }
        [JsonProperty("thumbLargeUrl")] public string ThumbLargeUrl { get; set; }
        [JsonProperty("thumbLargeTnWidth")] public int ThumbLargeTnWidth { get; set; }
        [JsonProperty("thumbLargeTnHeight")] public int ThumbLargeTnHeight { get; set; }
        [JsonProperty("thumbLargeTnUrl")] public string ThumbLargeTnUrl { get; set; }
        [JsonProperty("siteName")] public string SiteName { get; set; }
        [JsonProperty("siteLogo")] public string SiteLogo { get; set; }
        [JsonProperty("siteUrl")] public string SiteUrl { get; set; }
        [JsonProperty("fromUrl")] public string FromUrl { get; set; }
        [JsonProperty("isBook")] public string IsBook { get; set; }
        [JsonProperty("bookId")] public string BookId { get; set; }
        [JsonProperty("objUrl")] public string ObjUrl { get; set; }
        [JsonProperty("shareUrl")] public string ShareUrl { get; set; }
        [JsonProperty("setId")] public string SetId { get; set; }
        [JsonProperty("albumId")] public string AlbumId { get; set; }
        [JsonProperty("isAlbum")] public int IsAlbum { get; set; }
        [JsonProperty("albumName")] public string AlbumName { get; set; }
        [JsonProperty("albumNum")] public int AlbumNum { get; set; }
        [JsonProperty("userId")] public string UserId { get; set; }
        [JsonProperty("isVip")] public int IsVip { get; set; }
        [JsonProperty("isDapei")] public int IsDapei { get; set; }
        [JsonProperty("dressId")] public string DressId { get; set; }
        [JsonProperty("dressBuyLink")] public string DressBuyLink { get; set; }
        [JsonProperty("dressPrice")] public int DressPrice { get; set; }
        [JsonProperty("dressDiscount")] public int DressDiscount { get; set; }
        [JsonProperty("dressExtInfo")] public string DressExtInfo { get; set; }
        [JsonProperty("dressTag")] public string DressTag { get; set; }
        [JsonProperty("dressNum")] public int DressNum { get; set; }
        [JsonProperty("objTag")] public string ObjTag { get; set; }
        [JsonProperty("dressImgNum")] public int DressImgNum { get; set; }
        [JsonProperty("hostName")] public string HostName { get; set; }
        [JsonProperty("pictureId")] public string PictureId { get; set; }
        [JsonProperty("pictureSign")] public string PictureSign { get; set; }
        [JsonProperty("dataSrc")] public string DataSrc { get; set; }
        [JsonProperty("contentSign")] public string ContentSign { get; set; }
        [JsonProperty("albumDi")] public string AlbumDi { get; set; }
        [JsonProperty("canAlbumId")] public string CanAlbumId { get; set; }
        [JsonProperty("albumObjNum")] public string AlbumObjNum { get; set; }
        [JsonProperty("appId")] public string AppId { get; set; }
        [JsonProperty("photoId")] public string PhotoId { get; set; }
        [JsonProperty("fromName")] public int FromName { get; set; }
        [JsonProperty("fashion")] public string Fashion { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
    }

    public class BaiduImageJsonObject
    {
        [JsonProperty("col")] public string Col { get; set; }
        [JsonProperty("tag")] public string Tag { get; set; }
        [JsonProperty("tag3 ")] public string Tag3 { get; set; }
        [JsonProperty("sort")] public string Sort { get; set; }
        [JsonProperty("totalNum")] public int TotalNum { get; set; }
        [JsonProperty("startIndex")] public int StartIndex { get; set; }
        [JsonProperty("returnNumber")] public int ReturnNumber { get; set; }
        [JsonProperty("imgs")] public List<ImgsItemForBaidu> Imgs { get; set; }
    }
}
