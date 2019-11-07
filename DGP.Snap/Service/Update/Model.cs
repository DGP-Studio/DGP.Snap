using Newtonsoft.Json;
using System.Collections.Generic;

namespace DGP.Snap.Service.Update
{

    public class People
    {
        [JsonProperty("login")] public string Login { get; set; }
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("node_id")] public string Node_id { get; set; }
        [JsonProperty("avatar_url")] public string Avatar_url { get; set; }
        [JsonProperty("gravatar_id")] public string Gravatar_id { get; set; }
        [JsonProperty("url")] public string Url { get; set; }
        [JsonProperty("html_url")] public string Html_url { get; set; }
        [JsonProperty("followers_url")] public string Followers_url { get; set; }
        [JsonProperty("following_url")] public string Following_url { get; set; }
        [JsonProperty("gists_url")] public string Gists_url { get; set; }
        [JsonProperty("starred_url")] public string Starred_url { get; set; }
        [JsonProperty("subscriptions_url")] public string Subscriptions_url { get; set; }
        [JsonProperty("organizations_url")] public string Organizations_url { get; set; }
        [JsonProperty("repos_url")] public string Repos_url { get; set; }
        [JsonProperty("events_url")] public string Events_url { get; set; }
        [JsonProperty("received_events_url")] public string Received_events_url { get; set; }
        [JsonProperty("type")] public string Type { get; set; }
        [JsonProperty("site_admin")] public string Site_admin { get; set; }
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

    public class Release
    {
        [JsonProperty("url")] public string Url { get; set; }
        [JsonProperty("assets_url")] public string Assets_url { get; set; }
        [JsonProperty("upload_url")] public string Upload_url { get; set; }
        [JsonProperty("html_url")] public string Html_url { get; set; }
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("node_id")] public string Node_id { get; set; }
        [JsonProperty("tag_name")] public string TagName { get; set; }
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

    public enum UpdateAvailability
    {
        NeedUpdate = 0,
        IsNewestRelease = 1,
        IsInsiderVersion = 2,
        NotAvailable = 3
    }
}
