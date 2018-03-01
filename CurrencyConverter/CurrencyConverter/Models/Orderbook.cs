namespace CurrencyConverter.Models
{
    public class Orderbook
    {
        public string SymbolId { get; set; }

        public string TimeExchange { get; set; }

        public string TimeCoinapi { get; set; }

        public Ask[] Asks { get; set; }

        public Bid[] Bids { get; set; }
    }
}
