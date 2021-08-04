namespace ECommerce.Basket.Models
{
    public class ProductModel
    {
        public string Name { get; set; }
        public long Stock { get; set; }
        public byte Status { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
