using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HomeManager.Domain.Entities.Core;

namespace HomeManager.Domain.Entities.Vehicle
{
    public class FillUp : IEntity
    {
        [Key]
        public Guid Key { get; set; }
        [Required]
        public Guid CarKey { get; set; }
        [Required]
        public Guid PaymentMethodKey { get; set; }

        [Required]
        public DateTime Date { get; set; }
        [Required]
        public decimal Gallons { get; set; }
        [Required]
        public int TotalMileage { get; set; }
        [Required]
        public bool FullTank { get; set; }
        
        public Car Car { get; set; }
        public PaymentMethod PayementMethod { get; set; }
    }
}
