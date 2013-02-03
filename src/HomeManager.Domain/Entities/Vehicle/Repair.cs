using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HomeManager.Domain.Entities.Core;

namespace HomeManager.Domain.Entities.Vehicle
{
    public class Repair : IEntity
    {
        [Key]
        public Guid Key { get; set; }
        public Guid RepairShopKey { get; set; }

        public DateTime? EstimateDate { get; set; }
        public DateTime? RepairStartDate { get; set; }
        public DateTime? RepairCompleteDate { get; set; }

        public decimal? Estimate { get; set; }
        public decimal? TotalCost { get; set; }
        public string Description { get; set; }

        public byte[] Invoice { get; set; }

        public RepairShop Shop { get; set; }
    }
}
