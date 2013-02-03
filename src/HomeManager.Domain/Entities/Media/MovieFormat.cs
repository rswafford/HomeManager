using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Domain.Entities.Core;

namespace HomeManager.Domain.Entities.Media
{
    public class MovieFormat : IEntity
    {
        [Key]
        public Guid Key { get; set; }

        public string Description { get; set; }
        public bool PhysicalMedia { get; set; }
        public bool HighDefinition { get; set; }

        public string ImagePath { get; set; }

        [MaxLength(100)]
        public string Abbreviation { get; set; }
    }
}
