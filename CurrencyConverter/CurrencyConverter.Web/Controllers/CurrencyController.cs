using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CurrencyConverter.Web.Models;

namespace CurrencyConverter.Web.Controllers
{
    public class CurrencyController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("SignalRTest");
        }
    }
}
