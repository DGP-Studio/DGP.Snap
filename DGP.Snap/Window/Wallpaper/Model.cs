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

    public class BirdWallpaperJsonObject
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

}
