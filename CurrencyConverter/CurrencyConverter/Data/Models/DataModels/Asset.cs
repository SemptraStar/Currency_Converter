using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace CurrencyConverter.Data.Models.DataModels
{
    public class Asset
    {
        public string AssetId { get; set; }

        public string Name { get; set; }

        public bool IsTypeCrypto { get; set; }

        [JsonIgnore]
        public List<ExchangeRate> ExchangeRatesBase { get; set; }

        [JsonIgnore]
        public List<ExchangeRate> ExchangeRatesQuote { get; set; }
    }
}
