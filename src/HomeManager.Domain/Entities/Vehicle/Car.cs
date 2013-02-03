using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HomeManager.Domain.Entities.Core;

namespace HomeManager.Domain.Entities.Vehicle
{
    public class Car : IEntity
    {
        [Key]
        public Guid Key { get; set; }
        
        [MaxLength(50)]
        public string VehicleIdentificationNumber { get; set; }
        
        [Required]
        [MaxLength(8)]
        public string LicensePlateNumber { get; set; }
        
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }

        [Required]
        [MaxLength(4)]
        public string Year { get; set; }

        public DateTime PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }

        public DateTime? SellDate { get; set; }
        public decimal? SellPrice { get; set; }
    }
}
