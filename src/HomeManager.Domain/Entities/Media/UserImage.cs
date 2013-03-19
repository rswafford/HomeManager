using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Domain.Entities.Core;

namespace HomeManager.Domain.Entities.Media
{
    public class UserImage : IEntity
    {
        [Key]
        public Guid Key { get; set; }

        public Guid OwnerKey { get; set; }
        public Guid ImageKey { get; set; }

        [Required]
        [MaxLength(300)]
        public string Filename { get; set; }

        [Required]
        [MaxLength(8000)]
        public string FullPath { get; set; }

        public Image Image { get; set; }
        public User Owner { get; set; }
    }
}
