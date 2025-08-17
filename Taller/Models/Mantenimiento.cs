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

        // Relación con cliente
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("ClienteId")]
        public string ClienteId { get; set; }

        [BsonElement("TipoServicio")]
        public string TipoServicio { get; set; } // Ej: Cambio de aceite, Frenos, Motor

        [BsonElement("Descripcion")]
        public string Descripcion { get; set; }

        [BsonElement("Costo")]
        public decimal Costo { get; set; }

        [BsonElement("FechaIngreso")]
        public DateTime FechaIngreso { get; set; }

        [BsonElement("FechaEntrega")]
        public DateTime? FechaEntrega { get; set; } // nullable, porque puede no estar listo todavía

        [BsonElement("Estado")]
        public string Estado { get; set; } // Ej: Pendiente, En Proceso, Finalizado
         public string VehiculoId { get; set; }
        public string Servicio { get; set; }

        // <-- Asegúrate de tener esta propiedad
        public DateTime Fecha { get; set; }
    }
}
