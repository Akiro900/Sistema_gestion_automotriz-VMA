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

        public string ClienteId { get; set; }
        public string VehiculoId { get; set; }
        public string Servicio { get; set; }
        public DateTime FechaCita { get; set; }
        public string Estado { get; set; }
    }
}
