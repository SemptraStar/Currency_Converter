using System;

using Newtonsoft.Json;

namespace CurrencyConverter.Models
{
    public class Exchangerate
    {
        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("asset_id_base")]
        public string AssetIdBase { get; set; }

        [JsonProperty("asset_id_quote")]
        public string AssetIdQuote { get; set; }

        [JsonProperty("rate")]
        public decimal Rate { get; set; }
    }
}
