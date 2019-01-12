using System.Collections.Generic;
using Zafaran.Charity.AutomapperProfiles;

namespace Zafaran.Charity.Models
{
    public class Charity : BaseEntity,IHavePicture
    {
        public string Title { get; set; }
        public string PicturePath { get; set; }
        public string Description { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}