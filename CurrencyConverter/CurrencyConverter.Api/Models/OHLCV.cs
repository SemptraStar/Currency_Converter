using System;

using Newtonsoft.Json;

namespace CurrencyConverter.Models
{
    public class OHLCV
    {
        [JsonProperty("time_period_start")]
        public DateTime TimePeriodStart { get; set; }

        [JsonProperty("time_period_end")]
        public DateTime TimePeriodEnd { get; set; }

        [JsonProperty("time_open")]
        public DateTime TimeOpen { get; set; }

        [JsonProperty("time_close")]
        public DateTime TimeClose { get; set; }

        [JsonProperty("price_open")]
        public decimal PriceOpen { get; set; }

        [JsonProperty("price_high")]
        public decimal PriceHigh { get; set; }

        [JsonProperty("price_low")]
        public decimal PriceLow { get; set; }

        [JsonProperty("price_close")]
        public decimal PriceClose { get; set; }

        [JsonProperty("volume_traded")]
        public decimal VolumeTraded { get; set; }

        [JsonProperty("trades_count")]
        public int TradesCount { get; set; }
    }
}
