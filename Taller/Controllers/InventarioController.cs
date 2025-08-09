using Microsoft.AspNetCore.Mvc;

namespace Taller.Controllers
{
    public class InventarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
