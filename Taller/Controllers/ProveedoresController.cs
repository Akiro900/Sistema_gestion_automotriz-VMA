using Microsoft.AspNetCore.Mvc;
using Taller.Models;
using Taller.Services;
using System.Threading.Tasks;

namespace Taller.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly ProveedoresService _service;

        public ProveedoresController(ProveedoresService service)
        {
            _service = service;
        }

        // Listar Proveedores
        public async Task<IActionResult> Index()
        {
            var proveedores = await _service.ObtenerTodosAsync();
            return View(proveedores);
        }

        // Crear Proveedor
        public IActionResult Crear() => View();

        [HttpPost]
        public async Task<IActionResult> Crear(Proveedores proveedor)
        {
            if (ModelState.IsValid)
            {
                await _service.CrearAsync(proveedor);
                return RedirectToAction("Index");
            }
            return View(proveedor);
        }

        // Editar Proveedor
        public async Task<IActionResult> Editar(string id)
        {
            var proveedor = await _service.ObtenerPorIdAsync(id);
            if (proveedor == null) return NotFound();
            return View(proveedor);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(string id, Proveedores proveedor)
        {
            if (ModelState.IsValid)
            {
                await _service.ActualizarAsync(id, proveedor);
                return RedirectToAction("Index");
            }
            return View(proveedor);
        }

        // Eliminar Proveedor
        public async Task<IActionResult> Eliminar(string id)
        {
            await _service.EliminarAsync(id);
            return RedirectToAction("Index");
        }
    }
}
