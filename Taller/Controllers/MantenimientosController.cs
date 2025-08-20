using Microsoft.AspNetCore.Mvc;
using Taller.Models;
using Taller.Services;
using System.Threading.Tasks;

namespace Taller.Controllers
{
    public class MantenimientosController : Controller
    {
        private readonly MantenimientosService _mantenimientosService;
        private readonly VehiculosService _vehiculosService; 
        private readonly ClientesService _clientesService;

        public MantenimientosController(VehiculosService vehiculosService, MantenimientosService mantenimientosService)
        {
            _vehiculosService = vehiculosService;
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
            ViewBag.Servicios = new List<string> { "Cambio de aceite", "Frenos", "Alineación", "Otro" };

            var mantenimiento = new Mantenimiento
            {
                FechaIngreso = DateTime.Today,
                Estado = "Pendiente"
            };

            return View(mantenimiento);
        }

        // POST: Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Mantenimiento mantenimiento)
        {
            ViewBag.Servicios = new List<string> { "Cambio de aceite", "Frenos", "Alineación", "Otro" };

            

            // Validar que la placa exista
            var vehiculo = await _vehiculosService.GetByPlacaAsync(mantenimiento.Placa);
            if (vehiculo == null)
            {
                TempData["MensajeError"] = "La placa ingresada no existe en la base de datos.";
                return View(mantenimiento);
            }

            try
            {
                mantenimiento.FechaIngreso = DateTime.Now;
                mantenimiento.Estado = "Pendiente";

                await _mantenimientosService.CreateAsync(mantenimiento);
                TempData["MensajeExito"] = "Mantenimiento creado con exito";
                Console.WriteLine("Mantenimiento creado exitosamente");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear mantenimiento: {ex.Message}");
                TempData["MensajeError"] = "Error al crear mantenimiento, verifique la informacion ingresada.";
                return View(mantenimiento);
            }
        }


        // GET: Editar
        public async Task<IActionResult> Editar(string id)
        {
            var mantenimiento = await _mantenimientosService.GetByIdAsync(id);
            if (mantenimiento == null) return NotFound();

            // Lista de servicios para dropdown
            ViewBag.Servicios = new List<string> { "Cambio de aceite", "Frenos", "Alineación", "Otro" };
            return View(mantenimiento);
        }

        // POST: Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(string id, Mantenimiento mantenimiento)
        {
            if (id != mantenimiento.Id) return BadRequest();

            // Lista de servicios para dropdown
            ViewBag.Servicios = new List<string> { "Cambio de aceite", "Frenos", "Alineación", "Otro" };

            if (!ModelState.IsValid)
            {
                TempData["MensajeError"] = "Por favor corrige los errores del formulario.";
                return View(mantenimiento);
            }

            // Validar que la placa exista
            var vehiculo = await _vehiculosService.GetByPlacaAsync(mantenimiento.Placa);
            if (vehiculo == null)
            {
                TempData["MensajeError"] = "La placa ingresada no existe en la base de datos.";
                return View(mantenimiento);
            }

            try
            {
                await _mantenimientosService.UpdateAsync(id, mantenimiento);
                TempData["MensajeExito"] = "Mantenimiento actualizado con exito";
                Console.WriteLine("Mantenimiento actualizado correctamente");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar mantenimiento: {ex.Message}");
                TempData["MensajeError"] = "Error al actualizar mantenimiento, verifique los datos ingresados. ";
                return View(mantenimiento);
            }
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
            try
            {
                await _mantenimientosService.DeleteAsync(id);
                TempData["MensajeExito"] = "Mantenimiento eliminado con exito";
                Console.WriteLine("Mantenimiento eliminado correctamente");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar mantenimiento: {ex.Message}");
                TempData["MensajeError"] = "Error al eliminar mantenimiento, verifique los datos ingresados. ";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}
