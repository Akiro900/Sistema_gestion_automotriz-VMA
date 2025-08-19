using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Taller.Models
{
    public class Servicio
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required(ErrorMessage = "El nombre del servicio es obligatorio")]
        [Display(Name = "Nombre del Servicio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [DataType(DataType.Currency)]
        [Display(Name = "Precio")]
        public decimal Precio { get; set; }

        // ✅ Nuevo campo Estado
        [Required(ErrorMessage = "El estado es obligatorio")]
        [Display(Name = "Estado del Servicio")]
        public string Estado { get; set; } // Ejemplo: "Activo", "Inactivo"
    }
}
