using System;

namespace CurrencyConverter.Models
{
    public class Exchangerate
    {
        public DateTime Time { get; set; }

        public string AssetIdBase { get; set; }

        public string AssetIdQuote { get; set; }

        public decimal Rate { get; set; }
    }
}
