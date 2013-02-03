using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HomeManager.Domain.Entities.Core;

namespace HomeManager.Domain.Entities.Vehicle
{
    public class RepairShop : IEntity
    {
        [Key]
        public Guid Key { get; set; }

        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string WebsiteUrl { get; set; }
        public string PhoneNumber { get; set; }
    }
}
