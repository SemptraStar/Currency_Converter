using Newtonsoft.Json;

namespace CurrencyConverter.Models
{
    public class Exchange
    {
        [JsonProperty("symbol_id")]
        public string ExchangeId { get; set; }

        [JsonProperty("symbol_id")]
        public string Website { get; set; }

        [JsonProperty("symbol_id")]
        public string Name { get; set; }
    }
}
