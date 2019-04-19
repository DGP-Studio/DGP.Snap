using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DGP.snap.Service
{
    class UpdateService
    {
        
    }

    public class People
    {
        [JsonProperty("login")] public string login { get; set; }
        [JsonProperty("id")] public int id { get; set; }
        [JsonProperty("node_id")] public string node_id { get; set; }
        [JsonProperty("avatar_url")] public string avatar_url { get; set; }
        [JsonProperty("gravatar_id")] public string gravatar_id { get; set; }
        [JsonProperty("url")] public string url { get; set; }
        [JsonProperty("html_url")] public string html_url { get; set; }
        [JsonProperty("followers_url")] public string followers_url { get; set; }
        [JsonProperty("following_url")] public string following_url { get; set; }
        [JsonProperty("gists_url")] public string gists_url { get; set; }
        [JsonProperty("starred_url")] public string starred_url { get; set; }
        [JsonProperty("subscriptions_url")] public string subscriptions_url { get; set; }
        [JsonProperty("organizations_url")] public string organizations_url { get; set; }
        [JsonProperty("repos_url")] public string repos_url { get; set; }
        [JsonProperty("events_url")] public string events_url { get; set; }
        [JsonProperty("received_events_url")] public string received_events_url { get; set; }
        [JsonProperty("type")] public string type { get; set; }
        [JsonProperty("site_admin")] public string site_admin { get; set; }
    }

    public class AssetsItem
    {
        [JsonProperty("url")] public string Url { get; set; }
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("node_id")] public string Node_id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("label")] public string Label { get; set; }
        [JsonProperty("uploader")] public People Uploader { get; set; }
        [JsonProperty("content_type")] public string Content_type { get; set; }
        [JsonProperty("state")] public string State { get; set; }
        [JsonProperty("size")] public int Size { get; set; }
        [JsonProperty("download_count")] public int Download_count { get; set; }
        [JsonProperty("created_at")] public string Created_at { get; set; }
        [JsonProperty("updated_at")] public string Updated_at { get; set; }
        [JsonProperty("browser_download_url")] public string Browser_download_url { get; set; }
    }

    public class Root
    {
        [JsonProperty("url")] public string Url { get; set; }
        [JsonProperty("assets_url")] public string Assets_url { get; set; }
        [JsonProperty("upload_url")] public string Upload_url { get; set; }
        [JsonProperty("html_url")] public string Html_url { get; set; }
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("node_id")] public string Node_id { get; set; }
        [JsonProperty("tag_name")] public string Tag_name { get; set; }
        [JsonProperty("target_commitish")] public string Target_commitish { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("draft")] public string Draft { get; set; }
        [JsonProperty("author")] public People Author { get; set; }
        [JsonProperty("prerelease")] public string Prerelease { get; set; }
        [JsonProperty("created_at")] public string Created_at { get; set; }
        [JsonProperty("published_at")] public string Published_at { get; set; }
        [JsonProperty("assets")] public List<AssetsItem> Assets { get; set; }
        [JsonProperty("tarball_url")] public string Tarball_url { get; set; }
        [JsonProperty("zipball_url")] public string Zipball_url { get; set; }
        [JsonProperty("body")] public string Body { get; set; }
    }
}
