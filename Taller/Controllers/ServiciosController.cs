using Microsoft.AspNetCore.Mvc;

namespace Taller.Controllers
{
    public class ServiciosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
