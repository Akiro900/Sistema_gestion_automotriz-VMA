using Microsoft.AspNetCore.Mvc;

namespace Taller.Controllers
{
    public class CitasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
