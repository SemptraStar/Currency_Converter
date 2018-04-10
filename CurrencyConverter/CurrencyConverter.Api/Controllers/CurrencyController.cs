using System.Linq;

using CurrencyConverter.Api.Interfaces;
using CurrencyConverter.Api.Data;
using CurrencyConverter.Api.Data.Models.Currency;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Api.Controllers
{
    public class CurrencyController : Controller
    {
        private IDbContext _dbContext;

        private ICurrencyApi _currencyApi;

        public CurrencyController(
            IDbContext dbContext,
            ICurrencyApi currencyApi)
        {
            _dbContext = dbContext;
            _currencyApi = currencyApi;
        }

        [Route("api/currency/home")]
        public ActionResult Home()
        {
            return View();
        }

        [HttpGet]
        [Route("api/currency/assets")]
        public JsonResult GetAllAssets()
        {
            return Json(_dbContext.Set<Asset>().ToList());
        }

        [HttpGet]
        [Route("api/currency/exchangerate/{baseId}/{quoteId}")]
        public JsonResult GetExchangeRate(string baseId, string quoteId)
        {
            DbSet<Asset> assets = _dbContext.Set<Asset>();

            Asset baseAsset = assets.FirstOrDefault(x => x.AssetId == baseId);
            Asset quoteAsset = assets.FirstOrDefault(x => x.AssetId == quoteId);

            if (baseAsset == null)
            {
                return Json(new { Message = "Base asset not found" });
            }

            if (quoteAsset == null)
            {
                return Json(new { Message = "Quote asset not found" });
            }

            ExchangeRate rate = _currencyApi.GetSpecificRate(baseAsset, quoteAsset);

            return Json(new { Message = "Success", ExchangeRate = rate });
        }

        [HttpGet]
        [Route("api/currency/exchangerate/{baseId}/{quoteId}/{param}")]
        public JsonResult GetExchangeRate(string baseId, string quoteId, string param)
        {
            bool isBaseCrypto = false;
            bool isQuoteCrypto = false;

            switch (param)
            {
                case "crypto-to-crypto":
                    isBaseCrypto = true;
                    isQuoteCrypto = true;
                    break;
                case "base-to-crypto":
                    isQuoteCrypto = true;
                    break;
                case "crypto-to-quote":
                    isBaseCrypto = true;
                    break;
            }

            DbSet<Asset> assets = _dbContext.Set<Asset>();

            Asset baseAsset = assets.FirstOrDefault(x => x.AssetId == baseId 
                && x.IsTypeCrypto == isBaseCrypto);

            Asset quoteAsset = assets.FirstOrDefault(x => x.AssetId == quoteId 
                && x.IsTypeCrypto == isQuoteCrypto);

            if (baseAsset == null)
            {
                return Json(new { Message = "Base asset not found" });
            }

            if (quoteAsset == null)
            {
                return Json(new { Message = "Quote asset not found" });
            }

            ExchangeRate rate = _currencyApi.GetSpecificRate(baseAsset, quoteAsset);

            return Json(new { Message = "Success", ExchangeRate = rate });
        }
    }
}