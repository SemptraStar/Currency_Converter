using CurrencyConverter.Data.Models.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverter.Data
{
    public class CurrencyContext : DbContext, IDbContext
    {
        public DbSet<Asset> Assets { get; set; }

        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        public CurrencyContext(DbContextOptions<CurrencyContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>()
                .HasKey(x => new { x.AssetId, x.IsTypeCrypto });

            modelBuilder.Entity<ExchangeRate>()
                .HasOne(x => x.AssetBase)
                .WithMany(x => x.ExchangeRatesBase);

            modelBuilder.Entity<ExchangeRate>()
                .HasOne(x => x.AssetQuote)
                .WithMany(x => x.ExchangeRatesQuote);
        }
    }
}
