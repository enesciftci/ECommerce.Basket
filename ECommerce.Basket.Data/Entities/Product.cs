using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.Basket.Data.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public long Stock { get; set; }
        public decimal UnitPrice { get; set; }
        public byte Status { get; set; }
    }
}
