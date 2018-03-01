using System;

using Newtonsoft.Json;

namespace CurrencyConverter.Models
{
    public class Trade
    {
        [JsonProperty("symbol_id")]
        public string SymbolId { get; set; }

        [JsonProperty("time_exchange")]
        public DateTime TimeExchange { get; set; }

        [JsonProperty("time_coinapi")]
        public DateTime TimeCoinapi { get; set; }

        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("size")]
        public decimal Size { get; set; }

        [JsonProperty("taker_side")]
        public string TakerSide { get; set; }
    }

}
