using Microsoft.AspNetCore.Mvc;
using Taller.Models;
using Taller.Services;
using System.Threading.Tasks;

namespace Taller.Controllers
{
    public class FacturasController : Controller
    {
        private readonly FacturasService _service;

        public FacturasController(FacturasService service)
        {
            _service = service;
        }

        // Listar facturas
        public async Task<IActionResult> Index()
        {
            var facturas = await _service.GetAsync();
            return View(facturas);
        }

        // Crear factura
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Factura factura)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(factura);
                return RedirectToAction("Index");
            }
            return View(factura);
        }

        // Editar factura
        public async Task<IActionResult> Editar(string id)
        {
            var factura = await _service.GetAsync(id);
            if (factura == null) return NotFound();
            return View(factura);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(string id, Factura factura)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(id, factura);
                return RedirectToAction("Index");
            }
            return View(factura);
        }

        // Eliminar factura
        public async Task<IActionResult> Eliminar(string id)
        {
            var factura = await _service.GetAsync(id);
            if (factura == null) return NotFound();

            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
public async Task<IActionResult> Eliminar(string id, Factura factura)
{
    if (id != factura.Id) return BadRequest();

    await _service.DeleteAsync(id);
    return RedirectToAction("Index");
}

    }
}
