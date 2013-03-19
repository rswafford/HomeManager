using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Domain.Entities.Core;

namespace HomeManager.Domain.Entities.Media
{
    public class UserTvEpisode : IEntity
    {
        [Key]
        public Guid Key { get; set; }

        public Guid OwnerKey { get; set; }
        public Guid TvEpisodeKey { get; set; }

        [Required]
        [MaxLength(300)]
        public string Filename { get; set; }

        [Required]
        [MaxLength(8000)]
        public string FullPath { get; set; }

        [MaxLength(30)]
        public string ImportedFormatString { get; set; }

        public TvEpisode TvEpisode { get; set; }
        public User Owner { get; set; }
    }
}
