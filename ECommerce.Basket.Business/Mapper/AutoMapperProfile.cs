using AutoMapper;
using ECommerce.Basket.Data.Entities;
using ECommerce.Basket.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Basket.Business.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() : this("AutoMapperProfileMappings")
        {
        }

        public AutoMapperProfile(string profileName) : base(profileName)
        {
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<Data.Entities.Basket, BasketModel>().ReverseMap();
            CreateMap<BasketProduct, BasketProductModel>();
            CreateMap<ProductModel, BasketProduct>();
            CreateMap<Product, BasketProduct>();
        }
    }
}
