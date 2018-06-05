using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using CurrencyConverter.Api.Hubs;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.IdentityModel.Tokens;

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
            services.AddIdentity<IdentityUser, IdentityRole>();
            
            services.AddMvc();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.Configure<RazorViewEngineOptions>(o =>
            {
                // {2} is area, {1} is controller, {0} is the action    
                o.ViewLocationFormats.Clear();
                o.ViewLocationFormats.Add("/Controllers/{1}/Views/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add("/Controllers/Shared/Views/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add("/Web/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
            });

            services.AddDbContext<CurrencyContext>(options => options.UseSqlServer(Configuration["ConnectionString"]));
            services.AddScoped<IDbContext>(provider => provider.GetService<CurrencyContext>());

            services.AddTransient<ICurrencyApi, CurrencyApi>();
            
            services.AddSingleton<IBackgroundJobProcess, AssetsUpdateJob>();

            services.AddSignalR();

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

            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            
            app
                .UseAuthentication()
                .UseMvc(routes => 
                routes.MapRoute(
                    name: "default", 
                    template: "api/{controller=Currency}/{action=GetAllAssets}"
                )
                .MapRoute(
                    name: "web",
                    template: "{controller=Home}/{action=Index}"
                    ));

            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationsHub>("/api/notifications");
            });

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
