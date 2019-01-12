namespace Zafaran.Charity.ViewModels
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public string ProductTitle { get; set; }
        public int ProductPrice { get; set; }
        public int ProductSofrehPrice { get; set; }
        public string PicturePath { get; set; }
        public string ProductId { get; set; }
        public int TotalPrice { get; set; }
        public int TotalPriceSofre { get; set; }
    }
}