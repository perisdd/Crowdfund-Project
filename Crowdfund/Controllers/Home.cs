using Microsoft.AspNetCore.Mvc;

namespace Crowdfund.Controllers
{
    public class Home : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
