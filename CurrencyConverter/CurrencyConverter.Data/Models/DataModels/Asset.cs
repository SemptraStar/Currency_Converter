using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CurrencyConverter.Data.Models.DataModels
{
    public class Asset
    {
        [Key]
        public string AssetId { get; set; }

        public string Name { get; set; }

        public bool IsTypeCrypto { get; set; }

        public List<ExchangeRate> ExchangeRatesBase { get; set; }

        public List<ExchangeRate> ExchangeRatesQuote { get; set; }
    }
}
