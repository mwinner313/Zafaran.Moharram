using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Remotion.Linq.Parsing.ExpressionVisitors.TreeEvaluation;
using Zafaran.Charity.ViewModels;

namespace Zafaran.Charity.Controllers
{
    [Route("api/supporters")]
    public class SupportersController : Controller
    {
        private readonly AppDbContext _appDb;
        private readonly IMapper _mapper;
        public SupportersController(AppDbContext appDb, IMapper mapper)
        {
            _appDb = appDb;
            _mapper = mapper;
        }

        [HttpGet]
       [ProducesResponseType(typeof(SupporterViewModel),200)]
        public IActionResult Index()
        {
            return
            Ok(_mapper.Map<List<SupporterViewModel>>(_appDb.Supporters.ToList()));
        }
    }
}