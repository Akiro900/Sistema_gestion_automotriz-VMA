using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Taller.Models
{
    public class Factura
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Número de Factura")]
        public string NumeroFactura { get; set; }

        [Required]
        [Display(Name = "Cliente ID")]
        public string ClienteId { get; set; }

        [Required]
        [Display(Name = "Vehículo ID")]
        public string VehiculoId { get; set; }

        [Required]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required]
        [Display(Name = "Monto Total")]
        public decimal MontoTotal { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
    }
}
