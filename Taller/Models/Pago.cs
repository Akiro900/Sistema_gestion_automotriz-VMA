using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Taller.Models
{
    public class Pago
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Cliente")]
        public string ClienteNombre { get; set; }

        [Required]
        [Display(Name = "Monto")]
        [Range(0, double.MaxValue)]
        public decimal Monto { get; set; }

        [Required]
        [Display(Name = "Método de Pago")]
        public string MetodoPago { get; set; }

        [Required]
        [Display(Name = "Fecha de Pago")]
        [DataType(DataType.Date)]
        public DateTime FechaPago { get; set; }

        [Display(Name = "Estado")]
        public string Estado { get; set; } = "Pendiente"; // por defecto
    }
}
