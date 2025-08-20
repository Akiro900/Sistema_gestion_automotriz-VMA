using System;
using System.Collections.Generic;
using Taller.Models;
namespace Taller.Models
{
    public class ViewModels
    {
        public int TotalClientes { get; set; }
        public int TotalVehiculos { get; set; }
        public int TotalCitasPendientes { get; set; }
        public int TotalMantenimientosPendientes { get; set; }

        public List<Vehiculo> UltimosVehiculos { get; set; }
        public List<Citas> ProximasCitas { get; set; }
        public List<Mantenimiento> MantenimientosRecientes { get; set; }
    }
}
