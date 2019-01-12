using AutoMapper;
using Zafaran.Charity.Models;
using Zafaran.Charity.ViewModels;

namespace Zafaran.Charity.AutomapperProfiles
{
    public class SupporterProfile:Profile
    {
        public SupporterProfile()
        {
            CreateMap<Supporter, SupporterViewModel>();
        }
    }
}