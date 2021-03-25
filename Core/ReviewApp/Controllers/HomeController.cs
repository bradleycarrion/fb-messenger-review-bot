using Microsoft.AspNetCore.Mvc;

namespace ReviewApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
