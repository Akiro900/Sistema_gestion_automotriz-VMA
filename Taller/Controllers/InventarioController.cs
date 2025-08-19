using Microsoft.AspNetCore.Mvc;
using Taller.Models;
using Taller.Services;
using System.Threading.Tasks;

namespace Taller.Controllers
{
    public class InventarioController : Controller
    {
        private readonly InventarioService _service;

        public InventarioController(InventarioService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var inventario = await _service.ObtenerTodosAsync();
            return View(inventario);
        }

        // Crear
        public IActionResult Crear() => View();

        [HttpPost]
        public async Task<IActionResult> Crear(Inventario inventario)
        {
            if (ModelState.IsValid)
            {
                await _service.CrearAsync(inventario);
                return RedirectToAction("Index");
            }
            return View(inventario);
        }

        // Editar
        public async Task<IActionResult> Editar(string id)
        {
            var inventario = await _service.ObtenerPorIdAsync(id);
            if (inventario == null) return NotFound();
            return View(inventario);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(string id, Inventario inventario)
        {
            if (ModelState.IsValid)
            {
                await _service.ActualizarAsync(id, inventario);
                return RedirectToAction("Index");
            }
            return View(inventario);
        }

        // Eliminar
        public async Task<IActionResult> Eliminar(string id)
        {
            await _service.EliminarAsync(id);
            return RedirectToAction("Index");
        }
    }
}

