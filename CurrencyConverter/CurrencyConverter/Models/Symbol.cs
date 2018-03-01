using System;

using Newtonsoft.Json;

namespace CurrencyConverter.Models
{
    public class Symbol
    {
        [JsonProperty("symbol_id")]
        public string SymbolId { get; set; }

        [JsonProperty("exchange_id")]
        public string ExchangeId { get; set; }

        [JsonProperty("symbol_type")]
        public string SymbolType { get; set; }

        [JsonProperty("option_type_is_call")]
        public bool IsOptionTypeCall { get; set; }

        [JsonProperty("option_strike_price")]
        public decimal OptionStrikePrice { get; set; }

        [JsonProperty("option_contract_unit")]
        public decimal OptionContractUnit { get; set; }

        [JsonProperty("option_exercise_style")]
        public string OptionExerciseStyle { get; set; }

        [JsonProperty("option_expiration_time")]
        public DateTime OptionExpirationTime { get; set; }

        [JsonProperty("future_delivery_time")]
        public DateTime FutureDeliveryTime { get; set; }

        [JsonProperty("asset_id_base")]
        public string AssetIdBase { get; set; }

        [JsonProperty("asset_id_quote")]
        public string AssetIdQuote { get; set; }
    }
}
