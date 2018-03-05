using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;

using CurrencyConverter.Api;
using CurrencyConverter.Api.Interfaces;
using CurrencyConverter.Data;
using CurrencyConverter.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<CurrencyContext>(options => options.UseSqlServer(Configuration["ConnectionString"]));
            services.AddScoped<IDbContext>(provider => provider.GetService<CurrencyContext>());

            services.AddTransient<ICoinApi>(_ => new CoinApi(Configuration["ApiKey"]));
            services.AddTransient<IUahNBUApi, UahNBUApi>();

            services.AddSingleton<IHostedService, AssetsUpdateService>();
            services.AddSingleton<IHostedService, ExchangeRatesUpdateService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes => 
                routes.MapRoute(
                    name: "default", 
                    template: "api/{controller=Currency}/{action=GetAllAssets}"
                ));
        }
    }
}
