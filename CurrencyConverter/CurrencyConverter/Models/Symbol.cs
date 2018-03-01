using System;

namespace CurrencyConverter.Models
{
    public class Symbol
    {
        public string SymbolId { get; set; }

        public string ExchangeId { get; set; }

        public string SymbolType { get; set; }

        public bool IsOptionTypeCall { get; set; }

        public decimal OptionStrikePrice { get; set; }

        public decimal OptionContractUnit { get; set; }

        public string OptionExerciseStyle { get; set; }

        public DateTime OptionExpirationTime { get; set; }

        public DateTime FutureDeliveryTime { get; set; }

        public string AssetIdBase { get; set; }

        public string AssetIdQuote { get; set; }
    }
}
