using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CurrencyConverter.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CurrencyContext>
    {
        public CurrencyContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CurrencyContext>();
            var connectionString = "Server=tcp:easyconvert-db-server.database.windows.net,1433;Initial Catalog=easyconvert_db;Persist Security Info=False;User ID=semptra;Password=Ktifcfxtr19983;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            builder.UseSqlServer(connectionString);

            return new CurrencyContext(builder.Options);
        }
    }
}
