using AutoMapper;
using ECommerce.Basket.Api.Features.Basket;
using ECommerce.Basket.Business.Exceptions;
using ECommerce.Basket.Business.Services;
using ECommerce.Basket.Business.Services.Abstract;
using ECommerce.Basket.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ECommerce.Basket.Test
{
    public class BasketControllerTest
    {
        private Mock<IMediator> _mediator;
        private Mock<IBasketService> _basketService;
        private Mock<IProductService> _productService;
        private Mock<ILogger<PostAddToBasketCommandHandler>> _logger;
        private Mock<IMapper> _mapper;
        public BasketControllerTest()
        {
            _mediator = new Mock<IMediator>();
            _basketService = new Mock<IBasketService>();
            _productService = new Mock<IProductService>();
            _logger = new Mock<ILogger<PostAddToBasketCommandHandler>>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task AddToBasket_Success_Result()
        {
            var userId = "61097dd82087644c4deaa10c";
            var productId = "6109e7946fb8709274a2d76f";
            var command = new PostAddToBasketCommand(userId, productId);

            _mediator.Setup(x => x.Send(command, new CancellationToken())).
                ReturnsAsync(new BasketModel());

            var basketController = new BasketController(_mediator.Object);
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(new Claim[]
                {
                    new Claim("id",userId ),
                }));
            basketController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            //Action

            var result = await basketController.AddToBasket("61097dd82087644c4deaa10c");

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
