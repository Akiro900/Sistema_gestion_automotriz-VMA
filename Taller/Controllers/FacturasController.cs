using Microsoft.AspNetCore.Mvc;

namespace Taller.Controllers
{
    public class FacturasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
