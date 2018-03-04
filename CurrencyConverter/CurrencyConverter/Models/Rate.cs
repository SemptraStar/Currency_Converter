using System;

using Newtonsoft.Json;

namespace CurrencyConverter.Models
{
    public class Rate
    {
        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("asset_id_quote")]
        public string AssetIdQuote { get; set; }

        [JsonProperty("rate")]
        public decimal RateAmount { get; set; }
    }
}
