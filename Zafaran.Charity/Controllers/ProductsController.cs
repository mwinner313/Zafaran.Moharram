using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zafaran.Charity.ViewModels;

namespace Zafaran.Charity.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public ProductsController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProductViewModel),200)]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<List<ProductViewModel>>(_appDbContext.Products.ToList()));
        }
    }
}