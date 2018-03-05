using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using Newtonsoft.Json;

using CurrencyConverter.Api.Interfaces;
using CurrencyConverter.Models;

namespace CurrencyConverter.Api
{
    public class UahNBUApi : IUahNBUApi
    {
        private string _dateFormat = "yyyyMMdd";

        private static string WebUrl = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json"; // https://bank.gov.ua/control/uk/publish/article?art_id=38441973

        public T GetData<T>(string url)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient client = new HttpClient(handler, false))
                {
                    var responseFromServer = client.GetAsync(WebUrl + url).Result.Content.ReadAsStringAsync().Result;
                    var dataFromServer = JsonConvert.DeserializeObject<T>(responseFromServer);
                    return dataFromServer;
                }
            }
        }

        public List<NbuExchange> ExchangeRates()
        {
            return GetData<List<NbuExchange>>("");
        }
        public List<NbuExchange> ExchangeRates(DateTime date)
        {
            var url = string.Format("&date={0}", date.ToString(_dateFormat));
            return GetData<List<NbuExchange>>(url);
        }

        public NbuExchange ExchangeRatesGetSpecificRate(string asset)
        {
            var url = string.Format("&valcode={0}", asset);
            return GetData<List<NbuExchange>>(url).First();
        }
        public NbuExchange ExchangeRatesGetSpecificRate(string asset, DateTime date)
        {
            var url = string.Format("&valcode={0}&date={1}", asset, date.ToString(_dateFormat));
            return GetData<List<NbuExchange>>(url).First();
        }
    }
}
