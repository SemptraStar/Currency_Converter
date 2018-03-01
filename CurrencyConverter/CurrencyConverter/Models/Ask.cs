using Newtonsoft.Json;

namespace CurrencyConverter.Models
{
    public class Ask
    {
        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("size")]
        public decimal Size { get; set; }
    }
}
