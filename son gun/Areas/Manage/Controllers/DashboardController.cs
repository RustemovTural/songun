using Microsoft.AspNetCore.Mvc;

namespace son_gun.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class DashboardController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
    }
}
