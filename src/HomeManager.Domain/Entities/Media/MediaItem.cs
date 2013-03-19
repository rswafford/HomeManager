using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HomeManager.Domain.Entities.Core;

namespace HomeManager.Domain.Entities.Media
{
    public abstract class MediaItem : IEntity
    {
        [Key]
        public Guid Key { get; set; }

        [MaxLength(300)]
        public string Title { get; set; }

        public string FileHash { get; set; }
        
        protected MediaItem()
        {
        }
    }
}
