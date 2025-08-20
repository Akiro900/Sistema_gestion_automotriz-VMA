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
            

            try
            {
                await _vehiculosService.CreateAsync(vehiculo);

                TempData["MensajeExito"] = "Vehiculo creado con exito";
                return RedirectToAction(nameof(Index));
            }
            catch (MongoDB.Driver.MongoWriteException ex) when (ex.WriteError.Category == MongoDB.Driver.ServerErrorCategory.DuplicateKey)
            {
                TempData["MensajeError"] = "Ya existe un vehiculo con esa placa.";
                return View(vehiculo);
            }
            catch (Exception ex)
            {
                TempData["MensajeError"] = "Error al crear vehículo, verifique la informacion ingresada.";
                return View(vehiculo);
            }
        }


        // CU3: Editar
        // GET: Editar
        public async Task<IActionResult> Editar(string id)
        {
            var vehiculo = await _vehiculosService.GetByIdAsync(id);
            if (vehiculo == null) return NotFound();

            var clientes = await _clientesService.GetAsync();
            ViewBag.Clientes = new SelectList(clientes, "Id", "Nombre", vehiculo.ClienteId);

            return View(vehiculo);
        }

        // POST: Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(string id, Vehiculo vehiculo)
        {
            if (id != vehiculo.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                TempData["MensajeError"] = "Por favor corrige los errores del formulario.";
                var clientes = await _clientesService.GetAsync();
                ViewBag.Clientes = new SelectList(clientes, "Id", "Nombre", vehiculo.ClienteId);
                return View(vehiculo);
            }

            try
            {
                await _vehiculosService.UpdateAsync(id, vehiculo);

                TempData["MensajeExito"] = "Vehiculo actualizado con exito";
                return RedirectToAction(nameof(Index));
            }
            catch (MongoDB.Driver.MongoWriteException ex) when (ex.WriteError.Category == MongoDB.Driver.ServerErrorCategory.DuplicateKey)
            {
                TempData["MensajeError"] = "Ya existe un vehiculo con esa placa.";
                var clientes = await _clientesService.GetAsync();
                ViewBag.Clientes = new SelectList(clientes, "Id", "Nombre", vehiculo.ClienteId);
                return View(vehiculo);
            }
            catch (Exception ex)
            {
                TempData["MensajeError"] = "Error al actualizar vehiculo";
                var clientes = await _clientesService.GetAsync();
                ViewBag.Clientes = new SelectList(clientes, "Id", "Nombre", vehiculo.ClienteId);
                return View(vehiculo);
            }
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
