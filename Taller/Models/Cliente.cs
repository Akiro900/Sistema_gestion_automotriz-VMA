using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Taller.Models
{
    public class Cliente
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Nombre")]
        public string Nombre { get; set; }

        [BsonElement("Telefono")]
        public string Telefono { get; set; }

        [BsonElement("Correo")]
        public string Correo { get; set; }

        [BsonElement("Direccion")]
        public string Direccion { get; set; }

        [BsonElement("FechaRegistro")]
        public DateTime FechaRegistro { get; set; }
    }
}
