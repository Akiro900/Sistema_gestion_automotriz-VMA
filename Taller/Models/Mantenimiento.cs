using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Taller.Models
{
    public class Mantenimiento
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        // Relación con vehículo/cliente
        public string Placa { get; set; }

        // Servicio a realizar
        public string Servicio { get; set; } // Ej: Cambio de aceite, Frenos, Motor

        // Información opcional
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }

        // Fechas
        public DateTime FechaIngreso { get; set; } // Fecha de creación del mantenimiento
        public DateTime? FechaEntrega { get; set; } // Nullable, si aún no está listo

        // Estado
        public string Estado { get; set; } // Ej: Pendiente, En Proceso, Finalizado
    }
}
