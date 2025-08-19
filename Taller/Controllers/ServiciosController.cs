using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Taller.Models;
using Taller.Services;

namespace Taller.Controllers
{
    public class ServiciosController : Controller
    {
        private readonly ServiciosService _service;

        public ServiciosController(ServiciosService service)
        {
            _service = service;
        }

        // Index/Listar con filtros
        public async Task<IActionResult> Index(string nombre, string estado)
        {
            var servicios = await _service.ObtenerTodosAsync();

            // Filtros
            if (!string.IsNullOrEmpty(nombre))
                servicios = servicios.Where(s => s.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(estado))
                servicios = servicios.Where(s => s.Estado.Contains(estado, StringComparison.OrdinalIgnoreCase)).ToList();

            // Guardar filtros en ViewData para mantenerlos en la vista
            ViewData["NombreFilter"] = nombre;
            ViewData["EstadoFilter"] = estado;

            return View(servicios);
        }

        // Crear
        public IActionResult Crear() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                await _service.CrearAsync(servicio);
                TempData["Mensaje"] = "Servicio creado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(servicio);
        }

        // Editar
        public async Task<IActionResult> Editar(string id)
        {
            var servicio = await _service.ObtenerPorIdAsync(id);
            if (servicio == null) return NotFound();
            return View(servicio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(string id, Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                await _service.ActualizarAsync(id, servicio);
                TempData["Mensaje"] = "Servicio actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(servicio);
        }

        // Confirmar eliminación
        public async Task<IActionResult> Eliminar(string id)
        {
            var servicio = await _service.ObtenerPorIdAsync(id);
            if (servicio == null) return NotFound();
            return View(servicio); // Muestra confirmación
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(string id)
        {
            await _service.EliminarAsync(id);
            TempData["Mensaje"] = "Servicio eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
