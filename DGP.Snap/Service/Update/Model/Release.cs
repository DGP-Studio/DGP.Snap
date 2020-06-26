using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Snap.Service.Update.Model
{
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
}
