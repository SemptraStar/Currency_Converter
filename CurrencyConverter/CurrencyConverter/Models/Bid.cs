using Newtonsoft.Json;

namespace CurrencyConverter.Models
{
    public class Bid
    {
        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("size")]
        public decimal Size { get; set; }
    }

}
