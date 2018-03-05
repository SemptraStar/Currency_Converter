using Newtonsoft.Json;

namespace CurrencyConverter.Models
{
    public class Hashtag
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("indices")]
        public long[] Indices { get; set; }
    }
}
