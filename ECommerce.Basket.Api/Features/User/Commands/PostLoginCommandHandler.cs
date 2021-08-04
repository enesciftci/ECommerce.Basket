using ECommerce.Basket.Business.Services.Abstract;
using ECommerce.Basket.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Basket.Api.Features.User
{
    public class PostLoginCommandHandler : IRequestHandler<PostLoginCommand, TokenModel>
    {
        private readonly IUserService _userService;
        public PostLoginCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<TokenModel> Handle(PostLoginCommand request, CancellationToken cancellationToken)
        {
            return await _userService.Login(request.User);
        }
    }
}
