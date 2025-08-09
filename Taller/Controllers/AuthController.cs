using Microsoft.AspNetCore.Mvc;

namespace Taller.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // Por ahora solo simulamos autenticación
            if (email == "admin@taller.com" && password == "1234")
            {
                // Redirigir al home o dashboard
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Correo o contraseña incorrectos";
            return View();
        }
    }
}
