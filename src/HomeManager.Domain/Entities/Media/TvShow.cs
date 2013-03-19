using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Domain.Entities.Core;

namespace HomeManager.Domain.Entities.Media
{
    public class TvShow : IEntity
    {
        [Key]
        public Guid Key { get; set; }

        public string ShowName { get; set; }

        public DateTime? OriginalAirDate { get; set; }
        public virtual ICollection<TvEpisode> Episodes { get; set; }

        public TvShow()
        {
            Episodes = new HashSet<TvEpisode>();
        }
    }
}
