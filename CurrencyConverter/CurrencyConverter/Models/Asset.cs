using Newtonsoft.Json;

namespace CurrencyConverter.Models
{
    public class Asset
    {
        [JsonProperty("asset_id")]
        public string AssetId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type_is_crypto")]
        public bool IsTypeCrypto { get; set; }
    }
}
