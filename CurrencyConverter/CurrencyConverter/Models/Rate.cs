using System;

namespace CurrencyConverter.Models
{
    public class Rate
    {
        public DateTime Time { get; set; }

        public string AssetIdQuote { get; set; }

        public decimal RateAmount { get; set; }
    }

}
