using System;
using System.Collections.Generic;

using CurrencyConverter.Models;

namespace CurrencyConverter.Api.Interfaces
{
    public interface IUahNBUApi
    {
         T GetData<T>(string url);

         List<NbuExchange> ExchangeRates();
         List<NbuExchange> ExchangeRates(DateTime date);

         NbuExchange ExchangeRatesGetSpecificRate(string asset);
         NbuExchange ExchangeRatesGetSpecificRate(string asset, DateTime date);
    }
}
