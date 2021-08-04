using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECommerce.Basket.Data.Entities
{
    public class Basket
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public ICollection<BasketProduct> BasketProducts { get; set; }
        public decimal TotalPrice
        {
            get { return BasketProducts.Sum(p => p.TotalUnitPrice); }
        }
    }
}
