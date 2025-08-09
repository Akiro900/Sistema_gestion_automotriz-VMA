using Microsoft.AspNetCore.Mvc;

namespace Taller.Controllers
{
    public class PagosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
