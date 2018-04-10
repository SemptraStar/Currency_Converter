using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;

using CurrencyConverter.Api.Json;
using CurrencyConverter.Api.Data.Models.Currency;

using Newtonsoft.Json;

namespace CurrencyConverter.Api.ThirdParty
{
    public class CryptocompareApi
    {
        private string _WebUrl = @"https://min-api.cryptocompare.com/data";

        private string GetData(string url)
        {
            using (var handler = new HttpClientHandler())
            {
                using (var client = new HttpClient(handler, false))
                {
                    string responseFromServer = client.GetAsync(_WebUrl + url).Result.Content.ReadAsStringAsync().Result;

                    return responseFromServer;
                }
            }
        }
        private T GetDeserializedData<T>(string url)
        {
            using (var handler = new HttpClientHandler())
            {
                using (var client = new HttpClient(handler, false))
                {
                    string responseFromServer = client.GetAsync(_WebUrl + url).Result.Content.ReadAsStringAsync().Result;
                    T dataFromServer = JsonConvert.DeserializeObject<T>(responseFromServer);

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
            string exchangeRateData = GetData(string.Format(@"/price?fsym={0}&tsyms=USD", asset.AssetId));

            return JsonModelUtils.ExchangeRateDeserializeFromCryptoToUsd(asset, exchangeRateData);
        }
        public ExchangeRate GetCryptoRate(Asset baseAsset, Asset quoteAsset)
        {
            string exchangeRateData = GetData(string.Format(@"/price?fsym={0}&tsyms={1}", baseAsset.AssetId, quoteAsset.AssetId));

            return JsonModelUtils.ExchangeRateDeserializeFromCryptoToCrypto(baseAsset, quoteAsset, exchangeRateData);
        }
    }
}
