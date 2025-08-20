using Microsoft.AspNetCore.Mvc;
using Taller.Models;
using Taller.Services;
using System.Threading.Tasks;

namespace Taller.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly ProveedoresService _proveedoresService;

        public ProveedoresController(ProveedoresService service)
        {
            _proveedoresService = service;
        }

        public async Task<IActionResult> Index()
        {
            var proveedores = await _proveedoresService.ObtenerTodosAsync();
            return View(proveedores);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Proveedores proveedores)
        {


            proveedores.FechaRegistro = DateTime.Now;

            try
            {
                Console.WriteLine($"Intentando crear cliente: {proveedores.Nombre}");
                await _proveedoresService.CrearAsync(proveedores);
                Console.WriteLine("Proveedor creado exitosamente.");
                TempData["MensajeExito"] = "Proveedor creado con exito";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear proveedor: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Error al crear proveedor. Intente nuevamente.");
                TempData["MensajeError"] = "Error al crear proveedor, verifique la informacion ingresada.";
                return View(proveedores);
            }
        }

        public async Task<IActionResult> Editar(string id)
        {
            var proveedores = await _proveedoresService.ObtenerPorIdAsync(id);
            if (proveedores == null) return NotFound();
            return View(proveedores);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(string id, Proveedores proveedores)
        {
            if (!ModelState.IsValid)
            {
                TempData["MensajeError"] = "Por favor corrige los errores del formulario.";
                return View(proveedores);
            }

            try
            {
                var proveedorExistente = await _proveedoresService.ObtenerPorIdAsync(id);
                if (proveedorExistente == null) return NotFound();

                proveedores.Id = id; // asegurar id correcto
                proveedores.FechaRegistro = proveedorExistente.FechaRegistro;

                await _proveedoresService.ActualizarAsync(id, proveedores);

                TempData["MensajeExito"] = "Proveedor actualizado con exito";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["MensajeError"] = "Error al crear proveedor, verifique la informacion ingresada.";
                return View(proveedores);
            }
        }

        public async Task<IActionResult> Eliminar(string id)
        {
            var proveedores = await _proveedoresService.ObtenerPorIdAsync(id);
            if (proveedores == null) return NotFound();
            return View(proveedores);
        }

        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> EliminarConfirmado(string id)
        {
            await _proveedoresService.EliminarAsync(id);
            TempData["Mensaje"] = "Proveedor eliminado con exito";
            return RedirectToAction(nameof(Index));
        }
    }
}
