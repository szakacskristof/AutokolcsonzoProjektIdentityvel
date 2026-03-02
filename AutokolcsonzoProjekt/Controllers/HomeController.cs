using Microsoft.AspNetCore.Mvc;

namespace AutokolcsonzoProjekt.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
