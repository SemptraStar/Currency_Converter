using System;

namespace CurrencyConverter.Models
{
    public class OHLCV
    {
        public DateTime TimePeriodStart { get; set; }

        public DateTime TimePeriodEnd { get; set; }

        public DateTime TimeOpen { get; set; }

        public DateTime TimeClose { get; set; }

        public decimal PriceOpen { get; set; }

        public decimal PriceHigh { get; set; }

        public decimal PriceLow { get; set; }

        public decimal PriceClose { get; set; }

        public decimal VolumeTraded { get; set; }

        public int TradesCount { get; set; }
    }
}
