using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

namespace son_gun.Controllers
{
    public class HomeController : Controller
    {
     

        public IActionResult Index()
        {
            return View();
        }

      
    }
}
