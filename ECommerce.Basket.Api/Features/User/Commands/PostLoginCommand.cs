using ECommerce.Basket.Models;
using MediatR;

namespace ECommerce.Basket.Api.Features.User
{
    public class PostLoginCommand : IRequest<TokenModel>
    {
        public PostLoginCommand(Data.Entities.User user)
        {
            User = user;
        }
        public Data.Entities.User User { get; set; }
    }
}
