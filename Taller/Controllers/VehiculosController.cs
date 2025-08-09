using Microsoft.AspNetCore.Mvc;
using Taller.Models;
using Taller.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Taller.Controllers
{
    public class VehiculosController : Controller
    {
        private readonly VehiculosService _vehiculosService;
        private readonly ClientesService _clientesService;

        public VehiculosController(VehiculosService vehiculosService, ClientesService clientesService)
        {
            _vehiculosService = vehiculosService;
            _clientesService = clientesService;
        }

        // CU2: Listar vehículos
        public async Task<IActionResult> Index(string clienteId, string marca, string placa)
        {
            var vehiculos = await _vehiculosService.GetAsync();

            if (!string.IsNullOrEmpty(clienteId))
                vehiculos = vehiculos.Where(v => v.ClienteId == clienteId).ToList();

            if (!string.IsNullOrEmpty(marca))
                vehiculos = vehiculos.Where(v => v.Marca.Contains(marca, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(placa))
                vehiculos = vehiculos.Where(v => v.Placa.Contains(placa, StringComparison.OrdinalIgnoreCase)).ToList();

            var clientes = await _clientesService.GetAsync();
            ViewBag.Clientes = clientes;

            return View(vehiculos);
        }

        // CU1: Formulario de creación
        public async Task<IActionResult> Crear()
        {
            var clientes = await _clientesService.GetAsync();
            ViewBag.Clientes = new SelectList(clientes, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Vehiculo vehiculo)
        {
            

            await _vehiculosService.CreateAsync(vehiculo);
            return RedirectToAction(nameof(Index));
        }

        // CU3: Editar
        public async Task<IActionResult> Editar(string id)
        {
            var vehiculo = await _vehiculosService.GetByIdAsync(id);
            if (vehiculo == null) return NotFound();

            var clientes = await _clientesService.GetAsync();
            ViewBag.Clientes = new SelectList(clientes, "Id", "Nombre", vehiculo.ClienteId);

            return View(vehiculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(string id, Vehiculo vehiculo)
        {
            if (id != vehiculo.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                var clientes = await _clientesService.GetAsync();
                ViewBag.Clientes = new SelectList(clientes, "Id", "Nombre", vehiculo.ClienteId);
                return View(vehiculo);
            }

            await _vehiculosService.UpdateAsync(id, vehiculo);
            return RedirectToAction(nameof(Index));
        }

        // CU4: Eliminar
        public async Task<IActionResult> Eliminar(string id)
        {
            var vehiculo = await _vehiculosService.GetByIdAsync(id);
            if (vehiculo == null) return NotFound();
            return View(vehiculo);
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(string id)
        {
            await _vehiculosService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // CU5: Historial de Mantenimientos (link de momento)
        public async Task<IActionResult> Historial(string id)
        {
            // Aquí después conectaríamos con la colección de mantenimientos
            var vehiculo = await _vehiculosService.GetByIdAsync(id);
            if (vehiculo == null) return NotFound();

            ViewBag.Mantenimientos = new List<string> { "Cambio de aceite", "Alineación", "Revisión general" };
            return View(vehiculo);
        }
    }
}
