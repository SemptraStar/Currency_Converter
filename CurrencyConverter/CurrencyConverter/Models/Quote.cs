using System;

namespace CurrencyConverter.Models
{
    public class Quote
    {
        public string SymbolId { get; set; }

        public DateTime TimeExchange { get; set; }

        public DateTime TimeCoinapi { get; set; }

        public decimal AskPrice { get; set; }

        public decimal AskSize { get; set; }

        public decimal BidPrice { get; set; }

        public decimal BidSize { get; set; }

        public Trade LastTrade { get; set; }
    }
}
