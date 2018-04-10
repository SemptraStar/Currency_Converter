using CurrencyConverter.Api.Data.Models.Currency;
using CurrencyConverter.Api.Data.Models.Users;

using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Api.Data
{
    public class CurrencyContext : DbContext, IDbContext
    {
        public DbSet<Asset> Assets { get; set; }

        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Connection> Connections { get; set; }

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
