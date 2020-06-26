using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Snap.Service.Update.Model
{
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
}
