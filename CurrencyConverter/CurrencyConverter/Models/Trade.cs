using System;

namespace CurrencyConverter.Models
{
    public class Trade
    {
        public string SymbolId { get; set; }

        public DateTime TimeExchange { get; set; }

        public DateTime TimeCoinapi { get; set; }

        public string Uuid { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }

        public string TakerSide { get; set; }
    }

}
