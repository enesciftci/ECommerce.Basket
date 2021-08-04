using ECommerce.Basket.Models;
using MediatR;

namespace ECommerce.Basket.Api.Features.Basket
{
    public class PostAddToBasketCommand : IRequest<BasketModel>
    {
        public PostAddToBasketCommand(string userId, string productId)
        {
            ProductId = productId;
            UserId = userId;
        }

        public string ProductId { get; set; }
        public string UserId { get; set; }
    }
}
