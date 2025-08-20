using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Taller.Models;
using Taller.Services;

namespace Taller.Controllers
{
    public class FacturasController : Controller
    {
        private readonly FacturasService _facturasService;
        private readonly ClientesService _clientesService;
        private readonly VehiculosService _vehiculosService;

        public FacturasController(FacturasService facturasService, ClientesService clientesService, VehiculosService vehiculosService)
        {
            _facturasService = facturasService;
            _clientesService = clientesService;
            _vehiculosService = vehiculosService;
        }

        // GET: Index
        public async Task<IActionResult> Index()
        {
            var facturas = await _facturasService.GetAsync();
            return View(facturas);
        }


        // GET: Crear
        public async Task<IActionResult> Crear()
        {
            var random = Path.GetRandomFileName().Replace(".", "").Substring(0, 5).ToUpper();
            var numeroFactura = $"FACT-{DateTime.Now:yyyyMMdd}-{random}";

            var factura = new Factura
            {
                Fecha = DateTime.Today,
                NumeroFactura = numeroFactura
            };

            return View(factura);
        }

        // POST: Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Factura factura)
        {
            



            // Validar cliente
            var cliente = await _clientesService.GetByCedulaAsync(factura.ClienteId);
            if (cliente == null)
            {
                TempData["MensajeError"] = "La cedula ingresada no existe en la base de datos.";
                return View(factura);
            }

            // Validar vehículo
            var vehiculo = await _vehiculosService.GetByPlacaAsync(factura.VehiculoId);
            if (vehiculo == null)
            {
                TempData["MensajeError"] = "La placa ingresada no existe en la base de datos.";
                return View(factura);
            }

            if (factura.Fecha != DateTime.Today)
            {
                TempData["MensajeError"] = "La fecha de la factura debe ser igual al dia actual.";
                return View(factura);
            }


            try
            {
                
                await _facturasService.CreateAsync(factura);
                TempData["MensajeExito"] = "Factura creada con exito";
                Console.WriteLine("Factura creada exitosamente");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la factura: {ex.Message}");
                TempData["MensajeError"] = "Error al crear la factura, verifique los datos ingresados. ";
                return View(factura);
            }
        }

        // GET: Eliminar
        public async Task<IActionResult> Eliminar(string id)
        {
            var factura = await _facturasService.GetByIdAsync(id);
            if (factura == null) return NotFound();
            return View(factura);
        }

        // POST: Eliminar
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmed(string id)
        {
            await _facturasService.DeleteAsync(id);
            TempData["MensajeExito"] = "Factura eliminada con exito";
            return RedirectToAction(nameof(Index));
        }

    }
}
