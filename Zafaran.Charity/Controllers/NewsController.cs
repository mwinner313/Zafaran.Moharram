using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zafaran.Charity.ViewModels;

namespace Zafaran.Charity.Controllers
{
    [Route("api/news")]
    public class NewsController : Controller
    {
        private readonly AppDbContext _appDb;
        private readonly IMapper _mapper;

        public NewsController(AppDbContext appDb, IMapper mapper)
        {
            _appDb = appDb;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(TheNewsViewModel),200)]
        public IActionResult Get()
        {
            return
            Ok(_mapper.Map<List<TheNewsViewModel>>(_appDb.News.ToList()));
        }
    }
}