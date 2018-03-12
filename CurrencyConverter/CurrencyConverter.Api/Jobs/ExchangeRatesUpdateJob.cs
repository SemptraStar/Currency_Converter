using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using CurrencyConverter.Api.Interfaces;
using CurrencyConverter.Data;
using CurrencyConverter.Data.Models.DataModels;

namespace CurrencyConverter.Api.Jobs
{
    public class ExchangeRatesUpdateJob : BackgroundJobProcess
    {
        private IDbContext _dbContext;

        private ICurrencyApi _currencyApi;

        public ExchangeRatesUpdateJob(IDbContext dbContext, ICurrencyApi currencyApi)
        {
            _dbContext = dbContext;

            _currencyApi = currencyApi;
        }

        public override void Execute()
        {
            List<Asset> assets = _dbContext.Set<Asset>().ToList();

            foreach (var baseAsset in assets)
            {
                if (baseAsset.AssetId == null)
                    continue;

                var dbRates =  _dbContext.Set<ExchangeRate>()
                    .Include(x => x.AssetBase)
                    .Include(x => x.AssetQuote);

                foreach(var quoteAsset in assets.Where(x => x.AssetId != baseAsset.AssetId))
                {
                    var apiRate = _currencyApi.GetSpecificRate(baseAsset, quoteAsset);

                    if (apiRate == null || apiRate.Rate == 0)
                        return;

                    var dbRate = dbRates.FirstOrDefault(x =>
                        x.AssetBase.AssetId == baseAsset.AssetId &&
                        x.AssetQuote.AssetId == quoteAsset.AssetId);

                    if (dbRate == null)
                    {
                        _dbContext.Set<ExchangeRate>().Add(
                            new ExchangeRate
                            {
                                AssetBase = baseAsset,
                                AssetQuote = quoteAsset,
                                Rate = apiRate.Rate,
                                Time = apiRate.Time
                            });
                    }
                    else
                    {
                        dbRate.Rate = apiRate.Rate;
                        dbRate.Time = apiRate.Time;
                    }
                }

                _dbContext.SaveChanges();
            }
               
            _dbContext.SaveChanges();
        }
    }
}
