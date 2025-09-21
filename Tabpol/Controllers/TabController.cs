using Microsoft.AspNetCore.Mvc;

namespace Tabpol.Controllers
{
    public class TabController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
