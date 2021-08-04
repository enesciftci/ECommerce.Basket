using ECommerce.Basket.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Basket.Api.Features.Basket
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/basket")]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("addtobasket/{productId}")]
        public async Task<IActionResult> AddToBasket(string productId)
        {
            var userId = User.FindFirst("id").Value;
            var postAddToBasketCommand = new PostAddToBasketCommand(userId, productId);
            var result = await _mediator.Send(postAddToBasketCommand);
            return Ok(result);
        }
    }
}
