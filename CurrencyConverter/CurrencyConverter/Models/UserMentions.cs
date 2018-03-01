using Newtonsoft.Json;

namespace CurrencyConverter.Models
{
    public class UserMentions
    {
        [JsonProperty("indices")]
        public long[] Indices { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }
    }
}
