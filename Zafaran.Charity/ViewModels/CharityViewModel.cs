using Zafaran.Charity.Models;

namespace Zafaran.Charity.ViewModels
{
    public class CharityViewModel : IHavePicture
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PicturePath { get; set; }
        public string Description { get; set; }
    }
}