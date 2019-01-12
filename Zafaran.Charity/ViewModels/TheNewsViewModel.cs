using System;
using Zafaran.Charity.Models;

namespace Zafaran.Charity.ViewModels
{
    public class TheNewsViewModel : IHavePicture
    {
        public string PicturePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// 1 small
        /// 2 medium
        /// 3 large
        /// </summary>
        public PictureSize Size { get; set; }
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}