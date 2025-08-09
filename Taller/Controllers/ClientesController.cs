using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Taller.Models;
using Taller.Services;

namespace Taller.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ClientesService _clientesService;

        public ClientesController(ClientesService clientesService)
        {
            _clientesService = clientesService;
        }

        public async Task<IActionResult> Index()
        {
            var clientes = await _clientesService.GetAsync();
            return View(clientes);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Cliente cliente)
        {
            

            cliente.FechaRegistro = DateTime.Now;

            try
            {
                Console.WriteLine($"Intentando crear cliente: {cliente.Nombre}");
                await _clientesService.CreateAsync(cliente);
                Console.WriteLine("Cliente creado exitosamente.");
                TempData["Mensaje"] = "Cliente creado con éxito ✅";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear cliente: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Error al crear cliente. Intente nuevamente.");
                return View(cliente);
            }
        }

        public async Task<IActionResult> Editar(string id)
        {
            var cliente = await _clientesService.GetAsync(id);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(string id, Cliente cliente)
        {
            if (!ModelState.IsValid) return View(cliente);

            var clienteExistente = await _clientesService.GetAsync(id);
            if (clienteExistente == null) return NotFound();

            cliente.Id = id; // asegurar id correcto
            cliente.FechaRegistro = clienteExistente.FechaRegistro;

            await _clientesService.UpdateAsync(id, cliente);
            TempData["Mensaje"] = "Cliente actualizado con éxito ✅";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Eliminar(string id)
        {
            var cliente = await _clientesService.GetAsync(id);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> EliminarConfirmado(string id)
        {
            await _clientesService.DeleteAsync(id);
            TempData["Mensaje"] = "Cliente eliminado con éxito ✅";
            return RedirectToAction(nameof(Index));
        }
    }
}
