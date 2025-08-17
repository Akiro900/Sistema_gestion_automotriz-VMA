using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Taller.Models;
using Taller.Services;

namespace Taller.Controllers
{
    public class CitasController : Controller
    {
        private readonly CitasService _citasService;

        public CitasController(CitasService citasService)
        {
            _citasService = citasService;
        }

        // GET: Citas
        public async Task<IActionResult> Index(string clienteId, string estado)
        {
            var citas = await _citasService.GetAsync();

            if (!string.IsNullOrEmpty(clienteId))
                citas = citas.FindAll(c => c.ClienteId.Contains(clienteId));

            if (!string.IsNullOrEmpty(estado))
                citas = citas.FindAll(c => c.Estado.Contains(estado));

            ViewData["ClienteFilter"] = clienteId;
            ViewData["EstadoFilter"] = estado;

            return View(citas);
        }

        // GET: Citas/Crear
        public IActionResult Crear()
        {
            return View();
        }

        // POST: Citas/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Citas cita)
        {
            if (!ModelState.IsValid)
                return View(cita);

            await _citasService.CreateAsync(cita);
            return RedirectToAction(nameof(Index));
        }

        // GET: Citas/Editar/id
        public async Task<IActionResult> Editar(string id)
        {
            var cita = await _citasService.GetAsync(id);
            if (cita == null) return NotFound();

            return View(cita);
        }

        // POST: Citas/Editar/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(string id, Citas cita)
        {
            if (!ModelState.IsValid) return View(cita);

            await _citasService.UpdateAsync(id, cita);
            return RedirectToAction(nameof(Index));
        }

        // GET: Citas/Eliminar/id
        public async Task<IActionResult> Eliminar(string id)
        {
            var cita = await _citasService.GetAsync(id);
            if (cita == null) return NotFound();

            return View(cita);
        }

        // POST: Citas/Eliminar/id
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(string id)
        {
            await _citasService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
