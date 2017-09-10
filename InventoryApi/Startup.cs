using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using InventoryApi.Entities;
using InventoryApi.Utilities;
using InventoryApi.Services;
using AutoMapper;
using InventoryApi.Models;
using InventoryApi.Helpers;

namespace InventoryApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            // DI pala yung srvices pota - gor

            // seeding..
            services.AddDbContext<InventoryContext>(options => options.UseSqlServer(Configuration.GetConnectionString("InventoryDbConnectionString")));

            // injecting connection string
            services.AddSingleton<IConfiguration>(Configuration);

            //injecting services
            services.AddScoped<IProductsRepository, ProductRepository>();
            services.AddScoped<IShelvesRepository, ShelvesRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder => {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("API messed up!");
                    });
                });
            }
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            //AutoMapper.Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap<ProductVM, Product>();
            //    cfg.CreateMap<Product, ProductVM>()
            //        .ForMember(pVM => pVM.Url, p => p.ResolveUsing<ProductUrlResolver>());
            //});

            app.UseMvc();

            
            InventoryContextSeed.Initialize(app.ApplicationServices);
        }
    }
}
