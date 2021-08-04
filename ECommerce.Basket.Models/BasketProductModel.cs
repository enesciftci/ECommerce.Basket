namespace ECommerce.Basket.Models
{
    public class BasketProductModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalUnitPrice => UnitPrice * Quantity;
    }
}
