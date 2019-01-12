using AutoMapper;
using Zafaran.Charity.Models;
using Zafaran.Charity.ViewModels;

namespace Zafaran.Charity.AutomapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductViewModel>();
        }
    }
}