using System;

using Newtonsoft.Json;

namespace CurrencyConverter.Models
{
    public class NbuExchange
    {
        [JsonProperty("r030")]
        public string Id { get; set; }

        [JsonProperty("txt")]
        public string Name { get; set; }

        [JsonProperty("cc")]
        public string Asset { get; set; }

        [JsonProperty("rate")]
        public decimal Rate { get; set; }

        [JsonProperty("exchangedate")]
        public DateTime ExchangeDate { get; set; }

        public static explicit operator Exchangerate(NbuExchange nbuExchange)
        {
            return new Exchangerate
            {
                AssetIdBase = "UAH",
                AssetIdQuote = nbuExchange.Asset,
                Rate = nbuExchange.Rate,
                Time = nbuExchange.ExchangeDate
            };
        }
    }
}
