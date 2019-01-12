using System;

namespace Zafaran.Charity.Models
{
    public class Supporter:BaseEntity,IHavePicture
    {
        public string PicturePath { get; set; }
        public string Title { get; set; }
        public string Descriotion { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public PictureSize Size { get; set; }
    }
}