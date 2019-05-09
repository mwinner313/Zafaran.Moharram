namespace Zafaran.Charity.Models
{
    public class OrderItem : BaseEntity
    {
        public int Count { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ProductPrice { get; set; }
        public int ProductSofrehPrice { get; set; }
        public int TotalPrice { get; set; }
        public int TotalPriceSofre { get; set; }
    }
}