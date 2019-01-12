using AutoMapper;
using Zafaran.Charity.Models;
using Zafaran.Charity.ViewModels;

namespace Zafaran.Charity.AutomapperProfiles
{
    public class TheNewsProfile:Profile
    {
        public TheNewsProfile()
        {
            CreateMap<TheNews, TheNewsViewModel>();
        }
    }
}