namespace CurrencyConverter.Models
{
    public class Period
    {
        public string PeriodId { get; set; }

        public int LengthSeconds { get; set; }

        public int LengthMonths { get; set; }

        public int UnitCount { get; set; }

        public string UnitName { get; set; }

        public string DisplayName { get; set; }
    }
}
