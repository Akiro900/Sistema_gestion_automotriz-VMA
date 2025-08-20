using Microsoft.AspNetCore.Mvc;
using Taller.Models;
using Taller.Services;
using System.Threading.Tasks;

namespace Taller.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly EmpleadosService _empleadosService;

        public EmpleadosController(EmpleadosService service)
        {
            _empleadosService = service;
        }

        public async Task<IActionResult> Index()
        {
            var empleados = await _empleadosService.ObtenerTodosAsync();
            return View(empleados);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Empleado empleado)
        {


            

            try
            {
                Console.WriteLine($"Intentando crear empleado: {empleado.Nombre}");
                await _empleadosService.CrearAsync(empleado);
                Console.WriteLine("Empleado creado exitosamente.");
                TempData["MensajeExito"] = "Empleado creado con exito";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear empleado: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Error al crear empleado. Intente nuevamente.");
                TempData["MensajeError"] = "Error al crear empleado, verifique la informacion ingresada.";
                return View(empleado);
            }
        }

        public async Task<IActionResult> Editar(string id)
        {
            var empleado = await _empleadosService.ObtenerPorIdAsync(id);
            if (empleado == null) return NotFound();
            return View(empleado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(string id, Empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                TempData["MensajeError"] = "Por favor corrige los errores del formulario.";
                return View(empleado);
            }

            try
            {
                var empleadoExistente = await _empleadosService.ObtenerPorIdAsync(id);
                if (empleadoExistente == null) return NotFound();

                empleado.Id = id; // asegurar id correcto
              

                await _empleadosService.ActualizarAsync(id, empleado);

                TempData["MensajeExito"] = "Empleado actualizado con exito";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["MensajeError"] = "Error al crear empleado, verifique la informacion ingresada.";
                return View(empleado);
            }
        }

        public async Task<IActionResult> Eliminar(string id)
        {
            var empleado = await _empleadosService.ObtenerPorIdAsync(id);
            if (empleado == null) return NotFound();
            return View(empleado);
        }

        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> EliminarConfirmado(string id)
        {
            await _empleadosService.EliminarAsync(id);
            TempData["Mensaje"] = "Empleado eliminado con exito";
            return RedirectToAction(nameof(Index));
        }

    }
}
