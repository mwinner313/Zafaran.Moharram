using Zafaran.Charity.Models;

namespace Zafaran.Charity.ViewModels
{
    public class ProductViewModel : IHavePicture
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public int SofrehPrice { get; set; }
        public string PicturePath { get; set; }
        public string Description { get; set; }
    }
}