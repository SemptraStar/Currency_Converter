using CurrencyConverter.Api.Interfaces;
using CurrencyConverter.Data;
using CurrencyConverter.Data.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyConverter.Api.Services
{
    public class AssetsUpdateService : BackgroundService
    {
        private IDbContext _dbContext;

        private ICoinApi _coinApi;

        public AssetsUpdateService(IDbContext dbContext, ICoinApi coinApi)
        {
            _dbContext = dbContext;

            _coinApi = coinApi;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var dbAssets = _dbContext.Set<Asset>();

            await Task.Run(() =>
            {               
                var apiAssets = _coinApi.MetadataListAssets();

                foreach (var apiAsset in apiAssets)
                {
                    var dbAsset = dbAssets.FirstOrDefault(x => x.AssetId == apiAsset.AssetId);

                    if (dbAsset == null)
                    {
                        dbAssets.Add(
                            new Asset
                            {
                                AssetId = apiAsset.AssetId,
                                Name = apiAsset.Name,
                                IsTypeCrypto = apiAsset.IsTypeCrypto
                            });
                    }
                    else
                    {
                        dbAsset.Name = apiAsset.Name;
                        dbAsset.IsTypeCrypto = apiAsset.IsTypeCrypto;
                    }
                }
              
            }, stoppingToken)
            .ContinueWith(_ => _dbContext.SaveChanges());
        }

        public override void Dispose()
        {
            base.Dispose();

            _dbContext.Dispose();
        }
    }
}