using ECommerce.Basket.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Basket.Data.Entities;
using System.Threading;

namespace ECommerce.Basket.Api.Features.User
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Login([FromForm] Data.Entities.User user)
        {
            var postLoginCommand = new PostLoginCommand(user);
            var tokenModel = await _mediator.Send(postLoginCommand);
            if (tokenModel != null)
                return Ok(tokenModel);
            return Unauthorized("Kullanıcı adı veya şifre yanlış");
        }
    }
}
