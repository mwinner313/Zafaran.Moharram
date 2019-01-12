using Zafaran.Charity.AutomapperProfiles;

namespace Zafaran.Charity.Models
{
    public class Product : BaseEntity,IHavePicture
    {
        public string Title { get; set; }
        public int Price { get; set; }
        public int SofrehPrice { get; set; }
        public string PicturePath { get; set; }
        public string Description { get; set; }
    }
}