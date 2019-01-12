using AutoMapper;
using Microsoft.AspNetCore.Http;
using Zafaran.Charity.Models;
using Zafaran.Charity.ViewModels;

namespace Zafaran.Charity.AutomapperProfiles
{
    public class IHavePictureProfile:Profile
    {
        public IHavePictureProfile()
        {
            CreateMap<IHavePicture, IHavePicture>()
                .ForMember(x => x.PicturePath, opt => opt.ResolveUsing<PicBasePathResolver>())
                .Include<Product,ProductViewModel>()
                .Include<Models.Charity,CharityViewModel>()
                .Include<Supporter,SupporterViewModel>()
                .Include<TheNews,TheNewsViewModel>();
        }
    }

    public class PicBasePathResolver : IValueResolver<IHavePicture, IHavePicture, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PicBasePathResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Resolve(IHavePicture source, IHavePicture destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PicturePath)) return source.PicturePath;
            return _httpContextAccessor.HttpContext.Request.Host.Value + source.PicturePath;
        }
    }
}