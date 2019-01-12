using System;
using Zafaran.Charity.Models;

namespace Zafaran.Charity.ViewModels
{
    public class SupporterViewModel : IHavePicture
    {
        public int Id { get; set; }
        public string PicturePath { get; set; }
        public string Title { get; set; }
        public string Descriotion { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public PictureSize Size { get; set; }
    }
}