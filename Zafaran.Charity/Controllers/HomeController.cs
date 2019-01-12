using Microsoft.AspNetCore.Mvc;

namespace Zafaran.Charity.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}