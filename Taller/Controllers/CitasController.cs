using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Taller.Models;
using Taller.Services;

namespace Taller.Controllers
{
    public class CitasController : Controller
    {
        private readonly CitasService _citasService;
        private readonly ClientesService _clientesService;
        private readonly VehiculosService _vehiculosService;

        public CitasController(CitasService citasService, ClientesService clientesService, VehiculosService vehiculosService)
        {
            _citasService = citasService;
            _clientesService = clientesService;
            _vehiculosService = vehiculosService;
        }

        // GET: Crear
        public async Task<IActionResult> Crear()
        {
            ViewBag.Servicios = new List<string> { "Cambio de aceite", "Frenos", "Alineación", "Otro" };

            var cita = new Citas
            {
                FechaCita = DateTime.Today,
                Estado = "Pendiente"
            };

            return View(cita);
        }

        // POST: Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Citas cita)
        {
            ViewBag.Servicios = new List<string> { "Cambio de aceite", "Frenos", "Alineación", "Otro" };

            

            // Validar cliente
            var cliente = await _clientesService.GetByCedulaAsync(cita.ClienteId);
            if (cliente == null)
            {
                TempData["MensajeError"] = "La cedula ingresada no existe en la base de datos.";
                return View(cita);
            }

            // Validar vehículo
            var vehiculo = await _vehiculosService.GetByPlacaAsync(cita.VehiculoId);
            if (vehiculo == null)
            {
                TempData["MensajeError"] = "La placa ingresada no existe en la base de datos.";
                return View(cita);
            }

            if (cita.FechaCita <= DateTime.Today)
            {
                TempData["MensajeError"] = "La fecha de la cita debe ser posterior al dia actual.";
                return View(cita);
            }


            try
            {
                cita.Estado = "Pendiente";
                await _citasService.CreateAsync(cita);
                TempData["MensajeExito"] = "Cita creada con exito";
                Console.WriteLine("Cita creada exitosamente");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la cita: {ex.Message}");
                TempData["MensajeError"] = "Error al crear la cita, verifique los datos ingresados. ";
                return View(cita);
            }
        }

        // GET: Editar
        public async Task<IActionResult> Editar(string id)
        {
            var cita = await _citasService.GetByIdAsync(id);
            if (cita == null) return NotFound();

            ViewBag.Servicios = new List<string> { "Cambio de aceite", "Frenos", "Alineación", "Otro" };
            ViewBag.Estados = new List<string> { "Pendiente", "Confirmada", "Finalizada", "Cancelada" };

            return View(cita);
        }

        // POST: Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(string id, Citas cita)
        {
            if (id != cita.Id) return BadRequest();

            ViewBag.Servicios = new List<string> { "Cambio de aceite", "Frenos", "Alineación", "Otro" };
            ViewBag.Estados = new List<string> { "Pendiente", "Confirmada", "Finalizada", "Cancelada" };

            if (!ModelState.IsValid)
            {
                TempData["MensajeError"] = "Por favor corrige los errores del formulario.";
                return View(cita);
            }

            // Validar cliente
            var cliente = await _clientesService.GetByCedulaAsync(cita.ClienteId);
            if (cliente == null)
            {
                TempData["MensajeError"] = "La cedula ingresada no existe en la base de datos.";
                return View(cita);
            }

            // Validar vehículo
            var vehiculo = await _vehiculosService.GetByPlacaAsync(cita.VehiculoId);
            if (vehiculo == null)
            {
                TempData["MensajeError"] = "La placa ingresada no existe en la base de datos.";
                return View(cita);
            }

            try
            {
                await _citasService.UpdateAsync(id, cita);
                TempData["MensajeExito"] = "Cita actualizada con exito";
                Console.WriteLine("Cita actualizada correctamente");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar la cita: {ex.Message}");
                TempData["MensajeError"] = "Error al actualizar la cita, verifique los datos ingresados. ";
                return View(cita);
            }
        }

        // GET: Eliminar
        public async Task<IActionResult> Eliminar(string id)
        {
            var cita = await _citasService.GetByIdAsync(id);
            if (cita == null) return NotFound();
            return View(cita);
        }

        // POST: Eliminar
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmed(string id)
        {
            await _citasService.DeleteAsync(id);
            TempData["MensajeExito"] = "Cita eliminada con exito";
            return RedirectToAction(nameof(Index));
        }



        // GET: Index
        public async Task<IActionResult> Index()
        {
            var citas = await _citasService.GetAsync();
            return View(citas);
        }
    }
}
