using System;
using System.ComponentModel.DataAnnotations;

namespace CurrencyConverter.Data.Models.DataModels
{
    public class ExchangeRate
    {
        [Key]
        public int Id { get; set; }

        public Asset AssetBase { get; set; }

        public Asset AssetQuote { get; set; }

        public decimal Rate { get; set; }

        public DateTime Time { get; set; }
    }
}
