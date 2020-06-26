using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Snap.Service.Update.Model
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
}
