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

        [Required(ErrorMessage = "El nombre del proveedor es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El contacto es obligatorio")]
        public string Contacto { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        public string Direccion { get; set; }
    }
}

