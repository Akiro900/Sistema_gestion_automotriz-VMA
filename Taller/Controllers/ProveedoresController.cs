using Microsoft.AspNetCore.Mvc;

namespace Taller.Controllers
{
    public class ProveedoresController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
