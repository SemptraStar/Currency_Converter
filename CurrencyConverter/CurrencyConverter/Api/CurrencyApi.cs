using System;
using System.Collections.Generic;

using CurrencyConverter.Api.Interfaces;
using CurrencyConverter.Data.Models.Currency;

namespace CurrencyConverter.Api
{
    public class CurrencyApi : ICurrencyApi
    {
        private CryptocompareApi _cryptocompareApi;

        private DollarApi _dollarApi;

        private Asset _usdAsset = new Asset
        {
            AssetId = "USD",            
            Name = "United States dollar",
            IsTypeCrypto = false
        };

        public CurrencyApi()
        {
            _cryptocompareApi = new CryptocompareApi();

            _dollarApi = new DollarApi();
        }

        public List<Asset> GetAssets()
        {
            var assets = new List<Asset>();

            assets.AddRange(_cryptocompareApi.GetAssets());
            assets.AddRange(_dollarApi.GetAssets());

            return assets;
        }

        public ExchangeRate GetSpecificRate(Asset baseAsset, Asset quoteAsset)
        {
            if (baseAsset.IsTypeCrypto == true || quoteAsset.IsTypeCrypto == true)
            {
                return GetCryptoExchangeRate(baseAsset, quoteAsset);               
            }

            return GetRegularExchangeRate(baseAsset, quoteAsset);
        }

        private ExchangeRate GetCryptoExchangeRate(Asset cryptoAsset, Asset quoteAsset)
        {
            if (quoteAsset.IsTypeCrypto == true)
            {
                return _cryptocompareApi.GetCryptoRate(cryptoAsset, quoteAsset);
            }

            ExchangeRate cryptoToUsdRate = _cryptocompareApi.GetUsdRate(cryptoAsset);

            if (quoteAsset.AssetId == "USD")
                return cryptoToUsdRate;

            ExchangeRate quoteToUsdRate = _dollarApi.GetUsdRate(quoteAsset);

            return new ExchangeRate
            {
                AssetBase = cryptoAsset,
                AssetQuote = quoteAsset,
                Rate = cryptoToUsdRate.Rate * quoteToUsdRate.Rate,
                Time = DateTime.Now
            };
        }

        private ExchangeRate GetRegularExchangeRate(Asset baseAsset, Asset quoteAsset)
        {
            ExchangeRate baseToDollarRate = _dollarApi.GetUsdRate(baseAsset);

            ExchangeRate quoteToDollarRate = _dollarApi.GetUsdRate(quoteAsset);

            return new ExchangeRate
            {
                AssetBase = baseToDollarRate.AssetBase,
                AssetQuote = quoteToDollarRate.AssetBase,
                Rate = baseToDollarRate.Rate / quoteToDollarRate.Rate,
                Time = DateTime.Now
            };
        }     
    }
}
