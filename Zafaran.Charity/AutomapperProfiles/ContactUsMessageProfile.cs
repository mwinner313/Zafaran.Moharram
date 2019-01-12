using AutoMapper;
using Zafaran.Charity.Models;
using Zafaran.Charity.ViewModels;

namespace Zafaran.Charity.AutomapperProfiles
{
    public class ContactUsMessageProfile:Profile
    {
        public ContactUsMessageProfile()
        {
            CreateMap<SupportMessage, SupportMessageViewModel>();
            CreateMap<SupportMessageViewModel,SupportMessage >();
        }
    }
}