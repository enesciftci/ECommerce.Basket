using AutoMapper;
using ECommerce.Basket.Business.Mapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Basket.Api.Infrastructure
{
    public static class Mapper
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            services.AddSingleton(sp => config.CreateMapper());

            return services;
        }
    }
}
