using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Taller.Models
{
    public class Proveedores
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Nombre")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
        public string Nombre { get; set; }

        [BsonElement("Cedula")]
        [Required(ErrorMessage = "La cedula es obligatoria")]
        [StringLength(9, ErrorMessage = "La cedula no puede superar los 9 caracteres")]
        public string Cedula { get; set; }

        [BsonElement("Telefono")]
        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [Phone(ErrorMessage = "Ingrese un número de teléfono válido")]
        public string Telefono { get; set; }

        [BsonElement("Correo")]
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
        public string Correo { get; set; }

        [BsonElement("Direccion")]
        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(200, ErrorMessage = "La dirección no puede superar los 200 caracteres")]
        public string Direccion { get; set; }

        [BsonElement("FechaRegistro")]
        public DateTime FechaRegistro { get; set; }
    }
}

