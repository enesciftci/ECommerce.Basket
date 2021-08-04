using ECommerce.Basket.Business.Constants;
using ECommerce.Basket.Business.Services.Abstract;
using ECommerce.Basket.Data.Entities;
using ECommerce.Basket.Models.InfrastuctureModels;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace ECommerce.Basket.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;

        public ProductService(IMongoClient client, IECommerceDatabaseSettings settings)
        {
            _productCollection = client.GetDatabase(settings.DatabaseName).GetCollection<Product>(CollectionConstants.ProductCollection);
        }

        public async Task<Product> GetById(string id)
        {
            return await _productCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
    }
}
