using Microsoft.AspNetCore.Mvc;

namespace Trainee_Details.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
