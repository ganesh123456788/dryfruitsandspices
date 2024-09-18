namespace WebApplication8.Models
{
    public class CartItem
    {
        public string ProductName { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal GST { get; set; }
        public decimal DeliveryCharge { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
