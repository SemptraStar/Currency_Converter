using Newtonsoft.Json;

namespace CurrencyConverter.Models
{
    public class Orderbook
    {
        [JsonProperty("symbol_id")]
        public string SymbolId { get; set; }

        [JsonProperty("time_exchange")]
        public string TimeExchange { get; set; }

        [JsonProperty("time_coinapi")]
        public string TimeCoinapi { get; set; }

        [JsonProperty("asks")]
        public Ask[] Asks { get; set; }

        [JsonProperty("bids")]
        public Bid[] Bids { get; set; }
    }
}
