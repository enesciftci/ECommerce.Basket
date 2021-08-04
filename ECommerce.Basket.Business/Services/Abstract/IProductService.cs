using ECommerce.Basket.Data.Entities;
using System.Threading.Tasks;

namespace ECommerce.Basket.Business.Services.Abstract
{
    public interface IProductService
    {
        Task<Product> GetById(string id);
    }
}
