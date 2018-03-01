using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using CurrencyConverter.Api;
using CurrencyConverter.Models;

namespace CurrencyConverter.Controllers
{
    public class CurrencyController : Controller
    {
        private ICoinApi _coinApi;

        public CurrencyController(ICoinApi coinApi)
        {
            _coinApi = coinApi;
        }

        public IEnumerable<Twitter> Index()
        {
            return _coinApi.TwitterLastData(10);
        }

        [HttpGet]
        [Route("api/currency/assets")]
        public JsonResult GetAllAssets()
        {
            return Json(_coinApi.MetadataListAssets());
        }

        [HttpGet]
        [Route("api/currency/exchangerate/{baseId}/{quoteId}")]
        public JsonResult GetExchangeRate(string baseId, string quoteId)
        {
            return Json(_coinApi.ExchangeRatesGetSpecificRate(baseId, quoteId));
        }

        [HttpGet]
        [Route("api/currency/exchangerate/{baseId}/{quoteId}/{time}")]
        public JsonResult GetExchangeRate(string baseId, string quoteId, DateTime time)
        {
            return Json(_coinApi.ExchangeRatesGetSpecificRate(baseId, quoteId, time));
        }
    }
}