using AutoMapper;
using Zafaran.Charity.ViewModels;

namespace Zafaran.Charity.AutomapperProfiles
{
    public class CharityProfile : Profile
    {
        public CharityProfile()
        {
            CreateMap<Models.Charity, CharityViewModel>();
        }
    }
}