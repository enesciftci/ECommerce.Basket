using AutoMapper;
using ECommerce.Basket.Business.Exceptions;
using ECommerce.Basket.Business.Services.Abstract;
using ECommerce.Basket.Data.Entities;
using ECommerce.Basket.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Basket.Api.Features.Basket
{
    public class PostAddToBasketCommandHandler : IRequestHandler<PostAddToBasketCommand, BasketModel>
    {
        private readonly IBasketService _basketService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger<PostAddToBasketCommandHandler> _logger;

        public PostAddToBasketCommandHandler(
            IBasketService basketService,
            IProductService productService,
            IMapper mapper,
            ILogger<PostAddToBasketCommandHandler> logger)
        {
            _basketService = basketService;
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BasketModel> Handle(PostAddToBasketCommand request, CancellationToken cancellationToken)
        {
            var product = await _productService.GetById(request.ProductId);
            if (product == null)
                throw new NotificationException("Ürün bulunamadı");
            if (product.Stock == 0)
                throw new NotificationException("Yeterli stok bulunamadı.", "Uyarı");
            var basketProduct = _mapper.Map<BasketProduct>(product);
            basketProduct.Quantity = 1;
            await _basketService.AddToBasket(request.UserId, basketProduct);

            var basket = await _basketService.GetByUserId(request.UserId);

            using (_logger.BeginScope($"{request.UserId}")) { _logger.LogInformation("{@basket.Id} after add to basket. {@basket}", new object[] { basket.Id, basket }); }
            return _mapper.Map<BasketModel>(basket);
        }
    }
}
