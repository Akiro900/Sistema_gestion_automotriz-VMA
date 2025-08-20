using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Taller.Models;
using Taller.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Taller.Controllers
{
    public class InventarioController : Controller
    {
        private readonly InventarioService _inventarioService;

        public InventarioController(InventarioService inventarioService)
        {
            _inventarioService = inventarioService;
        }

        public async Task<IActionResult> Index()
        {
            var clientes = await _inventarioService.GetAsync();
            return View(clientes);
        }

        //Crear
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Inventario inventario)
        {


            inventario.FechaIngreso = DateTime.Now;

            try
            {
                Console.WriteLine($"Intentando crear inventario: {inventario.Nombre}");
                await _inventarioService.CreateAsync(inventario);
                Console.WriteLine("Inventario creado exitosamente.");
                TempData["MensajeExito"] = "Inventario creado con exito";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear inventario: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Error al crear inventario. Intente nuevamente.");
                TempData["MensajeError"] = "Error al crear inventario, verifique la informacion ingresada.";
                return View(inventario);
            }
        }


        public async Task<IActionResult> Editar(string id)
        {
            var inventario = await _inventarioService.GetAsync(id);
            if (inventario == null) return NotFound();
            return View(inventario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(string id, Inventario inventario)
        {
            if (!ModelState.IsValid)
            {
                TempData["MensajeError"] = "Por favor corrige los errores del formulario.";
                return View(inventario);
            }

            try
            {
                var inventarioExistente = await _inventarioService.GetAsync(id);
                if (inventarioExistente == null) return NotFound();

                inventario.Id = id; // asegurar id correcto
                inventario.FechaIngreso = inventarioExistente.FechaIngreso;

                await _inventarioService.UpdateAsync(id, inventario);

                TempData["MensajeExito"] = "Inventario actualizado con exito";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["MensajeError"] = "Error al crear Inventario, verifique la informacion ingresada.";
                return View(inventario);
            }
        }

        public async Task<IActionResult> Eliminar(string id)
        {
            var inventario = await _inventarioService.GetAsync(id);
            if (inventario == null) return NotFound();
            return View(inventario);
        }

        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> EliminarConfirmado(string id)
        {
            await _inventarioService.DeleteAsync(id);
            TempData["Mensaje"] = "Inventario eliminado con exito";
            return RedirectToAction(nameof(Index));
        }


    }
}
