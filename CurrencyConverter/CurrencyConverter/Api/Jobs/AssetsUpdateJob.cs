using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using CurrencyConverter.Api.Interfaces;
using CurrencyConverter.Data;
using CurrencyConverter.Data.Models.Currency;

using Newtonsoft.Json.Linq;

namespace CurrencyConverter.Api.Jobs
{
    public class AssetsUpdateJob : BackgroundJobProcess
    {
        private IDbContext _dbContext;

        private ICurrencyApi _currencyApi;

        private Dictionary<string, AssetData> _currencyCodeNames;

        public AssetsUpdateJob(IDbContext dbContext, ICurrencyApi currencyApi)
        {
            _dbContext = dbContext;

            _currencyApi = currencyApi;

            _currencyCodeNames = LoadCurrenciesNames();
        }

        public override void Execute()
        {
            var dbAssets = _dbContext.Set<Asset>();

            var apiAssets = _currencyApi.GetAssets()
                .GroupBy(x => new { x.AssetId, x.IsTypeCrypto })
                .Select(x => x.First());

            foreach (var apiAsset in apiAssets)
            {
                var dbAsset = dbAssets.FirstOrDefault(x => x.AssetId == apiAsset.AssetId 
                    && x.IsTypeCrypto == apiAsset.IsTypeCrypto);

                if (dbAsset == null)
                {
                    if (!apiAsset.IsTypeCrypto && _currencyCodeNames.ContainsKey(apiAsset.AssetId))
                    {
                        apiAsset.Name = _currencyCodeNames[apiAsset.AssetId].Name;
                    }

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

            _dbContext.SaveChanges();      
        }

        private Dictionary<string, AssetData> LoadCurrenciesNames()
        {
            var currenciesNames = new Dictionary<string, AssetData>();

            string jsonCurrencyData;

            using (var reader = new StreamReader("wwwroot/currencies.json"))
            {
                jsonCurrencyData = reader.ReadToEnd();
            }

            JObject currenciesObject = JObject.Parse(jsonCurrencyData);

            foreach (JProperty prop in currenciesObject.Properties())
            {
                var asset = prop.Value;

                currenciesNames.Add(prop.Name,
                    new AssetData
                    {
                        Name = asset["name"].ToString(),
                        Symbol = asset["symbol"].ToString(),
                        SymbolNavive = asset["symbol_native"].ToString()
                    });
            }

            return currenciesNames;
        }
    }

    internal class AssetData
    {
        public string Name { get; set; }

        public string Symbol { get; set; }

        public string SymbolNavive { get; set; }
    }
}