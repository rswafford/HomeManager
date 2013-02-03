using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HomeManager.Domain.Entities.Core;

namespace HomeManager.Domain.Entities.Media
{
    public abstract class MediaItem : IEntity
    {
        [Key]
        public Guid Key { get; set; }
        public Guid? OwnerKey { get; set; }

        [Required]
        [MaxLength(300)]
        public string Filename { get; set; }
        
        [Required]
        [MaxLength(8000)]
        public string FullPath { get; set; }

        [MaxLength(300)]
        public string Title { get; set; }

        public string FileHash { get; set; }

        public User Owner { get; set; }
    }
}
