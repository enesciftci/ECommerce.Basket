using System;

namespace ECommerce.Basket.Models
{
    public  class TokenModel
    {
        public string Id { get; set; }
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
