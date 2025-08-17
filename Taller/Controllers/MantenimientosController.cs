using Microsoft.AspNetCore.Mvc;
using Taller.Models;
using Taller.Services;
using System.Threading.Tasks;

namespace Taller.Controllers
{
    public class MantenimientosController : Controller
    {
        private readonly MantenimientosService _mantenimientosService;

        public MantenimientosController(MantenimientosService mantenimientosService)
        {
            _mantenimientosService = mantenimientosService;
        }

        public async Task<IActionResult> Index()
{
    var mantenimientos = await _mantenimientosService.GetAsync();
    return View(mantenimientos);
}

        // GET: Crear
        public IActionResult Crear()
        {
            return View();
        }

        // POST: Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Mantenimiento mantenimiento)
        {
            if (ModelState.IsValid)
            {
                await _mantenimientosService.CreateAsync(mantenimiento);
                return RedirectToAction(nameof(Index));
            }
            return View(mantenimiento);
        }

        // GET: Editar
        public async Task<IActionResult> Editar(string id)
        {
            var mantenimiento = await _mantenimientosService.GetByIdAsync(id);
            if (mantenimiento == null) return NotFound();
            return View(mantenimiento);
        }

        // POST: Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(string id, Mantenimiento mantenimiento)
        {
            if (ModelState.IsValid)
            {
                await _mantenimientosService.UpdateAsync(id, mantenimiento);
                return RedirectToAction(nameof(Index));
            }
            return View(mantenimiento);
        }

        // GET: Eliminar
        public async Task<IActionResult> Eliminar(string id)
        {
            var mantenimiento = await _mantenimientosService.GetByIdAsync(id);
            if (mantenimiento == null) return NotFound();
            return View(mantenimiento);
        }

        // POST: Eliminar
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(string id)
        {
            await _mantenimientosService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
