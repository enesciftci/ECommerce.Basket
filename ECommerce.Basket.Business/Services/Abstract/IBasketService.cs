using ECommerce.Basket.Data.Entities;
using System.Threading.Tasks;

namespace ECommerce.Basket.Business.Services.Abstract
{
    public interface IBasketService
    {
        Task AddToBasket(string userId, BasketProduct entity);
        Task<Data.Entities.Basket> GetByUserId(string userId);
        Task Update(Data.Entities.Basket entity);
    }
}
