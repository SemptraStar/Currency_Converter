namespace CurrencyConverter.Models
{
    public class ExchangeCurrentrate
    {
        public string AssetIdBase { get; set; }

        public Rate[] Rates { get; set; }
    }
}
