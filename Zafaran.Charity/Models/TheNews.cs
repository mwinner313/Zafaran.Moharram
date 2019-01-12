using System;
using Zafaran.Charity.AutomapperProfiles;

namespace Zafaran.Charity.Models
{
    public class TheNews:BaseEntity,IHavePicture
    {
        public string PicturePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public PictureSize Size { get; set; }
        public DateTime CreatedAt { get; set; }=DateTime.Now;
    }
}