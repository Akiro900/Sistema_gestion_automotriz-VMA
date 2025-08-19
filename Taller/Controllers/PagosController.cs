using Microsoft.AspNetCore.Mvc;
using Taller.Models;
using Taller.Services;

namespace Taller.Controllers
{
    public class PagosController : Controller
    {
        private readonly PagosService _pagosService;

        public PagosController(PagosService pagosService)
        {
            _pagosService = pagosService;
        }

        // GET: Pagos
        public IActionResult Index(string metodoFiltro, decimal? montoMin, decimal? montoMax)
        {
            var pagos = _pagosService.ObtenerTodos();

            // Filtros
            if (!string.IsNullOrEmpty(metodoFiltro))
                pagos = pagos.Where(p => p.MetodoPago.Contains(metodoFiltro, StringComparison.OrdinalIgnoreCase)).ToList();

            if (montoMin.HasValue)
                pagos = pagos.Where(p => p.Monto >= montoMin.Value).ToList();

            if (montoMax.HasValue)
                pagos = pagos.Where(p => p.Monto <= montoMax.Value).ToList();

            return View(pagos);
        }

        // GET: Pagos/Crear
        public IActionResult Crear() => View();

        // POST: Pagos/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Pago pago)
        {
            if (ModelState.IsValid)
            {
                _pagosService.Crear(pago);
                return RedirectToAction(nameof(Index));
            }
            return View(pago);
        }

        // GET: Pagos/Editar/5
        public IActionResult Editar(string id)
        {
            var pago = _pagosService.ObtenerPorId(id);
            if (pago == null) return NotFound();
            return View(pago);
        }

        // POST: Pagos/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(string id, Pago pago)
        {
            if (id != pago.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _pagosService.Editar(id, pago);
                return RedirectToAction(nameof(Index));
            }
            return View(pago);
        }

        // GET: Pagos/Eliminar/5
        public IActionResult Eliminar(string id)
        {
            var pago = _pagosService.ObtenerPorId(id);
            if (pago == null) return NotFound();
            return View(pago);
        }

        // POST: Pagos/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarConfirmado(string id)
        {
            _pagosService.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
