using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Taller.Models
{
    public class Citas
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string ClienteId { get; set; }  //  cédula
        public string VehiculoId { get; set; } //  placa


        [BsonElement("Servicio")]
        public string Servicio { get; set; }

        [BsonElement("FechaCita")]
        public DateTime FechaCita { get; set; }

        [BsonElement("Estado")]
        public string Estado { get; set; } // Ej: Pendiente, Confirmada, Cancelada
    }
}
