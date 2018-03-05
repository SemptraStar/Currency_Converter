using System;

using Newtonsoft.Json;

namespace CurrencyConverter.Models
{
    public class Quote
    {
        [JsonProperty("symbol_id")]
        public string SymbolId { get; set; }

        [JsonProperty("time_exchange")]
        public DateTime TimeExchange { get; set; }

        [JsonProperty("time_coinapi")]
        public DateTime TimeCoinapi { get; set; }

        [JsonProperty("ask_price")]
        public decimal AskPrice { get; set; }

        [JsonProperty("ask_size")]
        public decimal AskSize { get; set; }

        [JsonProperty("bid_price")]
        public decimal BidPrice { get; set; }

        [JsonProperty("bid_size")]
        public decimal BidSize { get; set; }

        [JsonProperty("last_trade")]
        public Trade LastTrade { get; set; }
    }
}
