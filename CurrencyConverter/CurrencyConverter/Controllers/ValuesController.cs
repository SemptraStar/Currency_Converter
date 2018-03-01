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
        // GET api/values
        // Example with twitter posts
        [HttpGet]
        public IEnumerable<string> Get()
        {
            CoinApi api = new CoinApi("6360037B-6081-47B6-8EB9-F8FFC17C9A47");

            return api.TwitterLastData().Take(10).Select(x => x.Text);
        }
    }
}
