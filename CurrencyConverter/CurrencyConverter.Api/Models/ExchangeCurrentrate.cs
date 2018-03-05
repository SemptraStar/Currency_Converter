using Newtonsoft.Json;

namespace CurrencyConverter.Models
{
    public class ExchangeCurrentrate
    {
        [JsonProperty("asset_id_base")]
        public string AssetIdBase { get; set; }

        [JsonProperty("rates")]
        public Rate[] Rates { get; set; }
    }
}
