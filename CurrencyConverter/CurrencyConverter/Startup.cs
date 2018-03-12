using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Hangfire;
using Hangfire.SqlServer;

using CurrencyConverter.Api;
using CurrencyConverter.Api.Interfaces;
using CurrencyConverter.Data;
using CurrencyConverter.Api.Jobs;

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

            services.AddTransient<ICurrencyApi, CurrencyApi>();
            
            services.AddSingleton<IBackgroundJobProcess, AssetsUpdateJob>();

            services.AddHangfire(options => options.UseSqlServerStorage(Configuration["ConnectionString"]));

            ConfigureJobs(services);
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

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }

        private void ConfigureJobs(IServiceCollection services)
        {
            JobStorage.Current = new SqlServerStorage(Configuration["ConnectionString"]);
            
            IEnumerable<IBackgroundJobProcess> jobs = services.BuildServiceProvider().GetServices<IBackgroundJobProcess>();

            IBackgroundJobProcess assetUpdateJob = jobs.FirstOrDefault(x => x.GetType() == typeof(AssetsUpdateJob));

            RecurringJob.AddOrUpdate(() => assetUpdateJob.Execute(), Cron.Daily);
        }
    }
}
