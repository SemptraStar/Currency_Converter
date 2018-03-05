using Newtonsoft.Json;

namespace CurrencyConverter.Models
{
    public class Entities
    {
        [JsonProperty("hashtags")]
        public Hashtag[] Hashtags { get; set; }

        [JsonProperty("user_mentions")]
        public UserMentions[] UserMentions { get; set; }

        [JsonProperty("urls")]
        public Url[] Urls { get; set; }
    }
}
