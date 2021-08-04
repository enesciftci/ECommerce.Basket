using AutoMapper;
using ECommerce.Basket.Business.Constants;
using ECommerce.Basket.Business.Services.Abstract;
using ECommerce.Basket.Data.Entities;
using ECommerce.Basket.Models;
using ECommerce.Basket.Models.InfrastuctureModels;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Basket.Business.Services
{
    public class BasketService : IBasketService
    {
        private readonly IMongoCollection<Data.Entities.Basket> _basketCollection;
        private readonly ILogger<BasketService> _logger;

        public BasketService(
            IMongoClient client,
            IECommerceDatabaseSettings settings,
            ILogger<BasketService> logger)
        {
            _basketCollection = client.GetDatabase(settings.DatabaseName).GetCollection<Data.Entities.Basket>(CollectionConstants.BasketCollection);
            _logger = logger;
        }

        public async Task AddToBasket(string userId, BasketProduct basketProduct)
        {
            var basket = await GetByUserId(userId);
            if (basket == null)
            {
                basket = new Data.Entities.Basket
                {
                    UserId = userId,
                    BasketProducts = new List<BasketProduct>
                    {
                        basketProduct
                    }
                };
                await _basketCollection.InsertOneAsync(basket);
                using (_logger.BeginScope($"UserId {userId}")) { _logger.LogInformation("Basket created for user. {@userId}", new object[] { userId, basket }); }
            }
            else
            {
                using (_logger.BeginScope($"UserId {userId}")) { _logger.LogInformation("{@basket.Id} before add to basket. {@basket} ", new object[] { basket.Id, basket }); }
                var currentProduct = basket.BasketProducts.FirstOrDefault(p => p.Id == basketProduct.Id);
                if (currentProduct == null)
                    basket.BasketProducts.Add(basketProduct);
                else
                    currentProduct.Quantity += basketProduct.Quantity;
                await Update(basket);
            }
        }

        public async Task<Data.Entities.Basket> GetByUserId(string userId)
        {
            return await _basketCollection.Find(p => p.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task Update(Data.Entities.Basket entity)
        {
            await _basketCollection.ReplaceOneAsync(p => p.Id == entity.Id, entity);
        }
    }
}
