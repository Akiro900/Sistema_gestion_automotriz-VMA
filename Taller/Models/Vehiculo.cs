using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Taller.Models
{
    public class Vehiculo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        public string Placa { get; set; }

        [Required]
        public string Marca { get; set; }

        [Required]
        public string Modelo { get; set; }

        [Required]
        public int Anio { get; set; }

        public string Color { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        public string ClienteId { get; set; }
    }
}
