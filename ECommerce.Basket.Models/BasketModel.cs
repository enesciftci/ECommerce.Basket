using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECommerce.Basket.Models
{
   public class BasketModel
    {
        public string UserId { get; set; }

        public List<BasketProductModel> BasketProducts { get; set; }

        public decimal TotalPrice => BasketProducts.Sum(p => p.TotalUnitPrice);
    }
}
