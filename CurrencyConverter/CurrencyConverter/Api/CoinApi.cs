using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

using Newtonsoft.Json;

using CurrencyConverter.Models;

namespace CurrencyConverter.Api
{
    public class CoinApi : ICoinApi
    {
        private string _apiKey;

        private string _dateFormat = "yyyy-MM-ddTHH:mm:ss.fff";

        private static string WebUrl = "https://rest.coinapi.io";// "https://rest-test.coinapi.io";

        public CoinApi(string apikey)
        {
            _apiKey = apikey;
        }

        public T GetData<T>(string url)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (HttpClient client = new HttpClient(handler, false))
                {
                    client.DefaultRequestHeaders.Add("X-CoinAPI-Key", _apiKey);

                    var responseFromServer = client.GetAsync(WebUrl + url).Result.Content.ReadAsStringAsync().Result;
                    var dataFromServer = JsonConvert.DeserializeObject<T>(responseFromServer);
                    return dataFromServer;
                }
            }
        }

        public List<Exchange> MetadataListExchanges() {
            return GetData<List<Exchange>>("/v1/exchanges");
        }

        public List<Asset> MetadataListAssets() {
            return GetData<List<Asset>>("/v1/assets");
        }
        public List<Symbol> MetadataListSymbols() {
            return GetData<List<Symbol>>("/v1/symbols");
        }

        public Exchangerate ExchangeRatesGetSpecificRate(string baseId, string quoteId, DateTime time) {
            var url = string.Format("/v1/exchangerate/{0}/{1}?time={2}", baseId, quoteId, time.ToString(_dateFormat));
            return GetData<Exchangerate>(url);
        }
        public Exchangerate ExchangeRatesGetSpecificRate(string baseId, string quoteId) {
            var url = string.Format("/v1/exchangerate/{0}/{1}", baseId, quoteId);
            return GetData<Exchangerate>(url);
        }

        public ExchangeCurrentrate ExchangeRatesGetAllCurrentRates(string baseId) {
            var url = string.Format("/v1/exchangerate/{0}", baseId);
            return GetData<ExchangeCurrentrate>(url);
        }

        public List<Period> OhlcvListAllPeriods() {
            var url = "/v1/ohlcv/periods";
            return GetData<List<Period>>(url);
        }

        public List<OHLCV> OhlcvLatestData(string symbolId, string periodId, int limit) {
            var url = string.Format("/v1/ohlcv/{0}/latest?period_id={1}&limit={2}", symbolId, periodId, limit);
            return GetData<List<OHLCV>>(url);
        }
        public List<OHLCV> OhlcvLatestData(string symbolId, string periodId) {
            var url = string.Format("/v1/ohlcv/{0}/latest?period_id={1}", symbolId, periodId);
            return GetData<List<OHLCV>>(url);
        }

        public List<OHLCV> OhlcvHistoricalData(string symbolId, string periodId, DateTime start, DateTime end, int limit) {
            var url = string.Format("/v1/ohlcv/{0}/history?period_id={1}&time_start={2}&time_end={3}&limit={4}", symbolId, periodId, start.ToString("yyyy-MM-dd HH:ss:mm"), end.ToString("yyyy-MM-dd HH:ss:mm"), limit);
            return GetData<List<OHLCV>>(url);
        }
        public List<OHLCV> OhlcvHistoricalData(string symbolId, string periodId, DateTime start, DateTime end) {
            var url = string.Format("/v1/ohlcv/{0}/history?period_id={1}&time_start={2}&time_end={3}", symbolId, periodId, start.ToString(_dateFormat), end.ToString(_dateFormat));
            return GetData<List<OHLCV>>(url);
        }
        public List<OHLCV> OhlcvHistoricalData(string symbolId, string periodId, DateTime start, int limit) {
            var url = string.Format("/v1/ohlcv/{0}/history?period_id={1}&time_start={2}&limit={3}", symbolId, periodId, start.ToString(_dateFormat), limit);
            return GetData<List<OHLCV>>(url);
        }
        public List<OHLCV> OhlcvHistoricalData(string symbolId, string periodId, DateTime start) {
            var url = string.Format("/v1/ohlcv/{0}/history?period_id={1}&time_start={2}", symbolId, periodId, start.ToString(_dateFormat));
            return GetData<List<OHLCV>>(url);
        }

        public List<Trade> TradesLatestDataAll() {
            var url = "/v1/trades/latest";
            return GetData<List<Trade>>(url);
        }
        public List<Trade> TradesLatestDataAll(int limit) {
            var url = string.Format("/v1/trades/latest?limit={0}", limit);
            return GetData<List<Trade>>(url);
        }

        public List<Trade> TradesLatestDataSymbol(string symbolId) {
            var url = string.Format("/v1/trades/{0}/latest", symbolId);
            return GetData<List<Trade>>(url);
        }
        public List<Trade> TradesLatestDataSymbol(string symbolId, int limit) {
            var url = string.Format("/v1/trades/{0}/latest?limit={1}", symbolId, limit);
            return GetData<List<Trade>>(url);
        }

        public List<Trade> TradesHistoricalData(string symbolId, DateTime start, DateTime end, int limit) {
            var url = string.Format("/v1/trades/{0}/history?time_start={1}&time_end={2}&limit={3}", symbolId, start.ToString(_dateFormat), end.ToString(_dateFormat), limit);
            return GetData<List<Trade>>(url);
        }
        public List<Trade> TradesHistoricalData(string symbolId, DateTime start) {
            var url = string.Format("/v1/trades/{0}/history?time_start={1}", symbolId, start.ToString(_dateFormat));
            return GetData<List<Trade>>(url);
        }
        public List<Trade> TradesHistoricalData(string symbolId, DateTime start, DateTime end) {
            var url = string.Format("/v1/trades/{0}/history?time_start={1}&time_end={2}", symbolId, start.ToString(_dateFormat), end.ToString(_dateFormat));
            return GetData<List<Trade>>(url);
        }
        public List<Trade> TradesHistoricalData(string symbolId, DateTime start, int limit) {
            var url = string.Format("/v1/trades/{0}/history?time_start={1}&limit={2}", symbolId, start.ToString(_dateFormat), limit);
            return GetData<List<Trade>>(url);
        }

        public List<Quote> QuotesCurrentDataAll() {
            var url = "/v1/quotes/current";
            return GetData<List<Quote>>(url);
        }

        public Quote QuotesCurrentDataSymbol(string symbolId) {
            var url = string.Format("/v1/quotes/{0}/current", symbolId);
            return GetData<Quote>(url);
        }

        public List<Quote> QuotesLatestDataAll() {
            var url = "/v1/quotes/latest";
            return GetData<List<Quote>>(url);
        }
        public List<Quote> QuotesLatestDataAll(int limit) {
            var url = string.Format("/v1/quotes/latest?limit={0}", limit);
            return GetData<List<Quote>>(url);
        }

        public List<Quote> QuotesLatestDataSymbol(string symbolId) {
            var url = string.Format("/v1/quotes/{0}/latest", symbolId);
            return GetData<List<Quote>>(url);
        }
        public List<Quote> QuotesLatestDataSymbol(string symbolId, int limit) {
            var url = string.Format("/v1/quotes/{0}/latest?limit={1}", symbolId, limit);
            return GetData<List<Quote>>(url);
        }

        public List<Quote> QuotesHistoricalData(string symbolId, DateTime start, DateTime end, int limit) {
            var url = string.Format("/v1/quotes/{0}/history?time_start={1}&time_end={2}&limit={3}", symbolId, start.ToString(_dateFormat), end.ToString(_dateFormat), limit);
            return GetData<List<Quote>>(url);

        }
        public List<Quote> QuotesHistoricalData(string symbolId, DateTime start) {
            var url = string.Format("/v1/quotes/{0}/history?time_start={1}", symbolId, start.ToString(_dateFormat));
            return GetData<List<Quote>>(url);
        }
        public List<Quote> QuotesHistoricalData(string symbolId, DateTime start, DateTime end) {
            var url = string.Format("/v1/quotes/{0}/history?time_start={1}&time_end={2}", symbolId, start.ToString(_dateFormat), end.ToString(_dateFormat));
            return GetData<List<Quote>>(url);
        }
        public List<Quote> QuotesHistoricalData(string symbolId, DateTime start, int limit) {
            var url = string.Format("/v1/quotes/{0}/history?time_start={1}&limit={2}", symbolId, start.ToString(_dateFormat), limit);
            return GetData<List<Quote>>(url);

        }

        public List<Orderbook> OrderbooksCurrentDataAll() {
            var url = "/v1/orderbooks/current";
            return GetData<List<Orderbook>>(url);
        }

        public Orderbook OrderbooksCurrentDataSymbol(string symbolId) {
            var url = string.Format("/v1/orderbooks/{0}/current", symbolId);
            return GetData<Orderbook>(url);
        }

        public List<Orderbook> OrderbooksLastData(string symbolId, int limit) {
            var url = string.Format("/v1/orderbooks/{0}/latest?limit={1}", symbolId, limit);
            return GetData<List<Orderbook>>(url);
        }
        public List<Orderbook> OrderbooksLastData(string symbolId) {
            var url = string.Format("/v1/orderbooks/{0}/latest", symbolId);
            return GetData<List<Orderbook>>(url);
        }

        public List<Orderbook> OrderbooksHistoricalData(string symbolId, DateTime start, DateTime end, int limit) {
            var url = string.Format("/v1/orderbooks/{0}/history?time_start={1}&time_end={2}&limit={3}", symbolId, start.ToString(_dateFormat), end.ToString(_dateFormat), limit);
            return GetData<List<Orderbook>>(url);
        }
        public List<Orderbook> OrderbooksHistoricalData(string symbolId, DateTime start) {
            var url = string.Format("/v1/orderbooks/{0}/history?time_start={1}", symbolId, start.ToString(_dateFormat));
            return GetData<List<Orderbook>>(url);
        }
        public List<Orderbook> OrderbooksHistoricalData(string symbolId, DateTime start, DateTime end) {
            var url = string.Format("/v1/orderbooks/{0}/history?time_start={1}&time_end={2}", symbolId, start.ToString(_dateFormat), end.ToString(_dateFormat));
            return GetData<List<Orderbook>>(url);
        }
        public List<Orderbook> OrderbooksHistoricalData(string symbolId, DateTime start, int limit) {
            var url = string.Format("/v1/orderbooks/{0}/history?time_start={1}&limit={2}", symbolId, start.ToString(_dateFormat), limit);
            return GetData<List<Orderbook>>(url);
        }

        public List<Twitter> TwitterLastData(int limit) {
            var url = string.Format("/v1/twitter/latest?limit={0}", limit);
            return GetData<List<Twitter>>(url);
        }
        public List<Twitter> TwitterLastData() {
            var url = "/v1/twitter/latest";
            return GetData<List<Twitter>>(url);
        }

        public List<Twitter> TwitterHistoricalData(DateTime start, DateTime end, int limit) {
            var url = string.Format("/v1/twitter/history?time_start={0}&time_end={1}&limit={2}", start.ToString(_dateFormat), end.ToString(_dateFormat), limit);
            return GetData<List<Twitter>>(url);
        }
        public List<Twitter> TwitterHistoricalData(DateTime start) {
            var url = string.Format("/v1/twitter/history?time_start={0}", start.ToString(_dateFormat));
            return GetData<List<Twitter>>(url);
        }
        public List<Twitter> TwitterHistoricalData(DateTime start, DateTime end) {
            var url = string.Format("/v1/twitter/history?time_start={0}&time_end={1}", start.ToString(_dateFormat), end.ToString(_dateFormat));
            return GetData<List<Twitter>>(url);
        }
        public List<Twitter> TwitterHistoricalData(DateTime start, int limit) {
            var url = string.Format("/v1/twitter/history?time_start={0}&limit={1}", start.ToString(_dateFormat), limit);
            return GetData<List<Twitter>>(url);
        }
    }
}
