using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using CurrencyConverter.Data.Models.Currency;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CurrencyConverter.Api.Json
{
    public static class JsonModelUtils
    {
        public static IEnumerable<Asset> AssetsDeserializeFromCryptocompare(string jsonResult)
        {
            JObject assetsData = JObject.Parse(jsonResult);

            foreach (JProperty asset in assetsData["Data"])
            {
                yield return AssetDeserializeFromCryptocompare(asset);
            }
        }
        public static Asset AssetDeserializeFromCryptocompare(JProperty assetData)
        {
            string id = assetData.Name;
            string name = (string)assetData.Values()
                .Cast<JProperty>()
                .First(x => x.Name == "CoinName")
                .Value;

            return new Asset
            {
                AssetId = id,
                Name = name,
                IsTypeCrypto = true
            };
        }

        public static IEnumerable<Asset> AssetsDeserializeFromUsd(string jsonResult)
        {
            JArray assetsData = JArray.Parse(jsonResult);

            foreach (JObject asset in assetsData)
            {
                yield return AssetDeserializeFromUsd(asset);
            }
        }
        public static Asset AssetDeserializeFromUsd(JObject assetData)
        {
            return new Asset
            {
                AssetId = assetData["currency_code"].ToString().ToUpper(),
                Name = assetData["name"].ToString(),
                IsTypeCrypto = false
            };
        }

        public static ExchangeRate ExchangeRateDeserializeFromCryptoToUsd(Asset asset, string exchangeRate)
        {
            dynamic exchangeRateData = JsonConvert.DeserializeObject(exchangeRate);

            var usdAsset = new Asset
            {
                AssetId = "USD",
                Name = "United States dollar",
                IsTypeCrypto = false
            };

            return new ExchangeRate
            {
                AssetBase = asset,
                AssetQuote = usdAsset,
                Rate = exchangeRateData.USD,
                Time = DateTime.Now
            };
        }

        public static ExchangeRate ExchangeRateDeserializeFromCryptoToCrypto(Asset baseAsset, Asset quoteAsset, string dataString)
        {
            JObject exchangeRateData = JObject.Parse(dataString);

            decimal rate = Decimal.Parse(exchangeRateData[quoteAsset.AssetId].ToString(),
                NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint);

            return new ExchangeRate
            {
                AssetBase = baseAsset,
                AssetQuote = quoteAsset,
                Rate = rate,
                Time = DateTime.Now
            };
        }

        public static ExchangeRate ExchangeRateDeserializeFromUsd(Asset baseAsset, string jsonResult)
        {
            JToken exchangeRateData = JArray.Parse(jsonResult)
                .FirstOrDefault(x => x["currency_code"].ToString().ToUpper() == baseAsset.AssetId);

            decimal rate = Convert.ToDecimal(exchangeRateData["rate"].ToString(), CultureInfo.InvariantCulture);

            return new ExchangeRate
            {
                AssetBase = baseAsset,
                AssetQuote = new Asset
                {
                    AssetId = exchangeRateData["currency_code"].ToString().ToUpper(),
                    Name = exchangeRateData["name"].ToString(),
                    IsTypeCrypto = false
                },
                Rate = rate,
                Time = DateTime.Now
            };
        }
    }
}
