using System.Collections.Generic;

using CurrencyConverter.Data.Models.Currency;

namespace CurrencyConverter.Api.Interfaces
{
    public interface ICurrencyApi
    {
        List<Asset> GetAssets();

        ExchangeRate GetSpecificRate(Asset baseAsset, Asset quoteAsset);
    }
}
