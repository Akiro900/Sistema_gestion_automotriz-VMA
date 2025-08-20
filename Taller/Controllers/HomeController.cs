using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Taller.Models;
using Taller.Services;

namespace Taller.Controllers
{
    public class HomeController : Controller


    {

        private readonly ClientesService _clientesService;
        private readonly VehiculosService _vehiculosService;
        private readonly CitasService _citasService;
        private readonly MantenimientosService _mantenimientosService;




        private readonly ILogger<HomeController> _logger;

       
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public HomeController(
    ILogger<HomeController> logger,
    ClientesService clientesService,
    VehiculosService vehiculosService,
    CitasService citasService,
    MantenimientosService mantenimientosService)
        {
            _logger = logger;
            _clientesService = clientesService;
            _vehiculosService = vehiculosService;
            _citasService = citasService;
            _mantenimientosService = mantenimientosService;
        }


        public async Task<IActionResult> Index()
        {
            var model = new ViewModels
            {
                TotalClientes = (await _clientesService.GetAllAsync()).Count,
                TotalVehiculos = (await _vehiculosService.GetAllAsync()).Count,
                TotalCitasPendientes = (await _citasService.GetPendientesAsync()).Count,
                TotalMantenimientosPendientes = (await _mantenimientosService.GetPendientesAsync()).Count,
                UltimosVehiculos = await _vehiculosService.GetUltimosAsync(5),
                ProximasCitas = await _citasService.GetProximasAsync(5),
                MantenimientosRecientes = await _mantenimientosService.GetRecientesAsync(5)
            };

            return View(model);
        }








    }
}
