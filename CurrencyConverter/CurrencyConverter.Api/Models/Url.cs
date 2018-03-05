using Newtonsoft.Json;

namespace CurrencyConverter.Models
{
    public class Url
    {
        [JsonProperty("indices")]
        public long[] Indices { get; set; }

        [JsonProperty("display_url")]
        public string DisplayUrl { get; set; }

        [JsonProperty("url")]
        public string UrlStr { get; set; }

        [JsonProperty("expanded_url")]
        public string ExpandedUrl { get; set; }
    }
}
