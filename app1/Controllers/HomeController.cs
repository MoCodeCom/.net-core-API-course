using Microsoft.AspNetCore.Mvc;

namespace app1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
