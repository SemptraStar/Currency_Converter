using System.Linq;

using Microsoft.AspNetCore.Mvc;

using CurrencyConverter.Api.Interfaces;
using CurrencyConverter.Data;
using CurrencyConverter.Data.Models.DataModels;

namespace CurrencyConverter.Controllers
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
            var assets = _dbContext.Set<Asset>();

            var baseAsset = assets.FirstOrDefault(x => x.AssetId == baseId);
            var quoteAsset = assets.FirstOrDefault(x => x.AssetId == quoteId);

            if (baseAsset == null || quoteAsset == null)
                return Json(new { Message = "Asset not found" });

            return Json(_currencyApi.GetSpecificRate(baseAsset, quoteAsset));
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

            var assets = _dbContext.Set<Asset>();

            var baseAsset = assets.FirstOrDefault(x => x.AssetId == baseId 
                && x.IsTypeCrypto == isBaseCrypto);

            var quoteAsset = assets.FirstOrDefault(x => x.AssetId == quoteId 
                && x.IsTypeCrypto == isQuoteCrypto);

            if (baseAsset == null || quoteAsset == null)
                return Json(new { Message = "Asset not found" });

            return Json(_currencyApi.GetSpecificRate(baseAsset, quoteAsset));
        }
    }
}