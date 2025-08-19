using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Taller.Models
{
    public class Empleado
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required]
        [Display(Name = "Correo")]
        public string Correo { get; set; }

        [Required]
        [Display(Name = "Cargo")]
        public string Cargo { get; set; }
    }
}
