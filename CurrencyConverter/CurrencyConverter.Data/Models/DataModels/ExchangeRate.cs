using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace CurrencyConverter.Data.Models.DataModels
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
