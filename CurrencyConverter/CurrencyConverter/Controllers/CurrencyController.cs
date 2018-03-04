using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using CurrencyConverter.Api.Interfaces;
using CurrencyConverter.Models;

namespace CurrencyConverter.Controllers
{
    public class CurrencyController : Controller
    {
        private ICoinApi _coinApi;

        private IUahNBUApi _uahNbuApi;

        public CurrencyController(ICoinApi coinApi, IUahNBUApi uahNBUApi)
        {
            _coinApi = coinApi;

            _uahNbuApi = uahNBUApi;
        }

        [HttpGet]
        [Route("api/currency/assets")]
        public JsonResult GetAllAssets()
        {
            return Json(_coinApi.MetadataListAssets());
        }

        [HttpGet]
        [Route("api/currency/exchangerate/{baseId}")]
        public JsonResult GetExchangeRate(string baseId)
        {
            return Json(_coinApi.ExchangeRatesGetAllCurrentRates(baseId));
        }

        [HttpGet]
        [Route("api/currency/exchangerate/{baseId}/{quoteId}")]
        public JsonResult GetExchangeRate(string baseId, string quoteId)
        {
            Exchangerate rate;

            if (baseId.ToUpper() == "UAH" || quoteId.ToUpper() == "UAH")
            {
                rate = GetUahExchangeRate(baseId, quoteId);
            }
            else
            {
                rate = _coinApi.ExchangeRatesGetSpecificRate(baseId, quoteId);
            }

            return Json(rate);
        }

        [HttpGet]
        [Route("api/currency/exchangerate/{baseId}/{quoteId}/{time}")]
        public JsonResult GetExchangeRate(string baseId, string quoteId, DateTime time)
        {
            Exchangerate rate;

            if (baseId.ToUpper() == "UAH" || quoteId.ToUpper() == "UAH")
            {
                rate = GetUahExchangeRate(baseId, quoteId, time);
            }
            else
            {
                rate = _coinApi.ExchangeRatesGetSpecificRate(baseId, quoteId, time);
            }

            return Json(rate);
        }

        private Exchangerate GetUahExchangeRate(string baseId, string quoteId, DateTime time)
        {
            Exchangerate rate = _coinApi.ExchangeRatesGetSpecificRate(baseId, quoteId, time);

            if (rate.Rate != 0)
                return rate;

            if (baseId.ToUpper() == "UAH")
            {
                rate = (Exchangerate)_uahNbuApi.ExchangeRatesGetSpecificRate(quoteId, time);
            }
            else if (quoteId.ToUpper() == "UAH")
            {
                rate = (Exchangerate)_uahNbuApi.ExchangeRatesGetSpecificRate(baseId, time);

                rate.AssetIdBase = baseId;
                rate.AssetIdQuote = "UAH";
            }

            return rate;
        }
        private Exchangerate GetUahExchangeRate(string baseId, string quoteId)
        {
            return GetUahExchangeRate(baseId, quoteId, DateTime.Now);
        }
    }
}