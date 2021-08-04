using ECommerce.Basket.Business.Services;
using ECommerce.Basket.Business.Services.Abstract;
using ECommerce.Basket.Models.InfrastuctureModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Basket.Api.Infrastructure
{
    public static class AppServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration.GetSection("Token"));
            services.Configure<ECommerceDatabaseSettings>(configuration.GetSection(nameof(ECommerceDatabaseSettings)));

            services.AddSingleton<IECommerceDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ECommerceDatabaseSettings>>().Value);

            services.AddSingleton<IAppSettings>(sp =>
                          sp.GetRequiredService<IOptions<AppSettings>>().Value);

            services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(configuration.GetSection("ECommerceDatabaseSettings").Get<ECommerceDatabaseSettings>().ConnectionString));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
