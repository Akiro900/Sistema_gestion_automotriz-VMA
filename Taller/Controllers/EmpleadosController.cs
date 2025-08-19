using Microsoft.AspNetCore.Mvc;
using Taller.Models;
using Taller.Services;
using System.Threading.Tasks;

namespace Taller.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly EmpleadosService _service;

        public EmpleadosController(EmpleadosService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var empleados = await _service.ObtenerTodosAsync();
            return View(empleados);
        }

        // Crear
        public IActionResult Crear() => View();

        [HttpPost]
        public async Task<IActionResult> Crear(Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                await _service.CrearAsync(empleado);
                return RedirectToAction("Index");
            }
            return View(empleado);
        }

        // Editar
        public async Task<IActionResult> Editar(string id)
        {
            var empleado = await _service.ObtenerPorIdAsync(id);
            if (empleado == null) return NotFound();
            return View(empleado);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(string id, Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                await _service.ActualizarAsync(id, empleado);
                return RedirectToAction("Index");
            }
            return View(empleado);
        }

        // Eliminar
        public async Task<IActionResult> Eliminar(string id)
        {
            await _service.EliminarAsync(id);
            return RedirectToAction("Index");
        }
    }
}
