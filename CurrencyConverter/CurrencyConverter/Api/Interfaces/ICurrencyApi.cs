using CurrencyConverter.Data.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverter.Api.Interfaces
{
    public interface ICurrencyApi
    {
        List<Asset> GetAssets();

        ExchangeRate GetSpecificRate(Asset baseAsset, Asset quoteAsset);
    }
}
