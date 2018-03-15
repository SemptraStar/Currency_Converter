using System;
using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace CurrencyConverter.Data.Models.Currency
{
    public class ExchangeRate
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public string AssetBaseId { get; set; }
        public Asset AssetBase { get; set; }

        [JsonIgnore]
        public string AssetQuoteId { get; set; }
        public Asset AssetQuote { get; set; }

        public decimal Rate { get; set; }

        public DateTime Time { get; set; }
    }
}
