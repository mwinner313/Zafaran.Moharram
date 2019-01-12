using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zafaran.Charity.ViewModels;

namespace Zafaran.Charity.Controllers
{
    [Route("api/charity")]
    public class CharitiesController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public CharitiesController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CharityViewModel),200)]
        public IActionResult Get()
        {
            return
                Ok(_mapper.Map<List<CharityViewModel>>(_appDbContext.Charities.ToList()));
        }
    }
}