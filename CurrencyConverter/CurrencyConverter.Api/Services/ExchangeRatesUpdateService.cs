using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CurrencyConverter.Api.Interfaces;
using CurrencyConverter.Data;
using CurrencyConverter.Data.Models.DataModels;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Api.Services
{
    public class ExchangeRatesUpdateService : BackgroundService
    {
        private IDbContext _dbContext;

        private ICoinApi _coinApi;

        public ExchangeRatesUpdateService(IDbContext dbContext, ICoinApi coinApi)
        {
            _dbContext = dbContext;

            _coinApi = coinApi;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<Asset> assets = _dbContext.Set<Asset>().ToList();

            await Task.Run(() =>
            {
                foreach (var asset in assets)
                {
                    var rates = _coinApi.ExchangeRatesGetAllCurrentRates(asset.AssetId);

                    foreach (var rate in rates.Rates)
                    {
                        var currentRate = _dbContext.Set<ExchangeRate>()
                            .Include(x => x.AssetBase)
                            .Include(x => x.AssetQuote)
                            .FirstOrDefault(x => x.AssetBase.AssetId == asset.AssetId && x.AssetQuote.AssetId == rate.AssetIdQuote);

                        if (currentRate == null)
                        {
                            _dbContext.Set<ExchangeRate>().Add(new ExchangeRate
                            {
                                AssetBase = asset,
                                AssetQuote = _dbContext.Set<Asset>().FirstOrDefault(x => x.AssetId == rate.AssetIdQuote),
                                Rate = rate.RateAmount,
                                Time = rate.Time
                            });
                        }
                        else
                        {
                            currentRate.Rate = rate.RateAmount;
                            currentRate.Time = rate.Time;
                        }                     
                    }
                }
               
                _dbContext.SaveChanges();
            }, stoppingToken);
        }

        public override void Dispose()
        {
            base.Dispose();

            _dbContext.Dispose();
        }
    }
}
