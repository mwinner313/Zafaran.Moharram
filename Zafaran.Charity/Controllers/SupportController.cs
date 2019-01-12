using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zafaran.Charity.Models;
using Zafaran.Charity.ViewModels;

namespace Zafaran.Charity.Controllers
{
    [Route("api/support")]
    public class SupportController:Controller
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _dbContext;

        public SupportController(IMapper mapper, AppDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SupportMessageViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values);
            _dbContext.Messages.Add(_mapper.Map<SupportMessage>(model));
            await _dbContext.SaveChangesAsync();
            return Ok(new {sucees=true});
        }
    }
}