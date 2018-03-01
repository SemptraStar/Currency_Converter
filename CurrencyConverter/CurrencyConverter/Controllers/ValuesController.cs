using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using CurrencyConverter.Api;
using CurrencyConverter.Models;

namespace CurrencyConverter.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private ICoinApi _coinApi;

        public ValuesController(ICoinApi coinApi)
        {
            _coinApi = coinApi;
        }

        // GET api/values
        // Example with twitter posts
        [HttpGet]
        public Trade Get()
        {
            return _coinApi.TradesLatestDataAll(1).FirstOrDefault();
        }
    }
}
