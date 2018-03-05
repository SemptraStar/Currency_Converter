using Newtonsoft.Json;

namespace CurrencyConverter.Models
{
    public class Period
    {
        [JsonProperty("period_id")]
        public string PeriodId { get; set; }

        [JsonProperty("length_seconds")]
        public int LengthSeconds { get; set; }

        [JsonProperty("length_months")]
        public int LengthMonths { get; set; }

        [JsonProperty("unit_count")]
        public int UnitCount { get; set; }

        [JsonProperty("unit_name")]
        public string UnitName { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
    }
}
