using Microsoft.AspNetCore.Mvc;

namespace ApiFinancias.Controllers.ControllerViews
{
    public class HomeController : Controller
    {        
        public IActionResult Index()
        {
            return View();
        }
    }
}
