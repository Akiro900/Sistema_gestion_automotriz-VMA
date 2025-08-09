using Microsoft.AspNetCore.Mvc;

namespace Taller.Controllers
{
    public class EmpleadosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
