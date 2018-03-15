using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;

using CurrencyConverter.Api.Json;
using CurrencyConverter.Data.Models.Currency;

using Newtonsoft.Json;

namespace CurrencyConverter.Api
{
    public class CryptocompareApi
    {
        private string _WebUrl = @"https://min-api.cryptocompare.com/data";

        private string GetData(string url)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient client = new HttpClient(handler, false))
                {
                    var responseFromServer = client.GetAsync(_WebUrl + url).Result.Content.ReadAsStringAsync().Result;

                    return responseFromServer;
                }
            }
        }
        private T GetDeserializedData<T>(string url)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient client = new HttpClient(handler, false))
                {
                    var responseFromServer = client.GetAsync(_WebUrl + url).Result.Content.ReadAsStringAsync().Result;
                    var dataFromServer = JsonConvert.DeserializeObject<T>(responseFromServer);

                    return dataFromServer;
                }
            }
        }

        public List<Asset> GetAssets()
        {
            string assetsData = GetData(@"/all/coinlist");

            return JsonModelUtils.AssetsDeserializeFromCryptocompare(assetsData).ToList();
        }

        public ExchangeRate GetUsdRate(Asset asset)
        {
            string exchangeRateData = GetData(String.Format(@"/price?fsym={0}&tsyms=USD", asset.AssetId));

            return JsonModelUtils.ExchangeRateDeserializeFromCryptoToUsd(asset, exchangeRateData);
        }
        public ExchangeRate GetCryptoRate(Asset baseAsset, Asset quoteAsset)
        {
            string exchangeRateData = GetData(String.Format(@"/price?fsym={0}&tsyms={1}", baseAsset.AssetId, quoteAsset.AssetId));

            return JsonModelUtils.ExchangeRateDeserializeFromCryptoToCrypto(baseAsset, quoteAsset, exchangeRateData);
        }
    }
}
