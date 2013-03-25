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

        [MaxLength(10)]
        public string ImdbId { get; set; }
        [MaxLength(10)]
        public string TvDbId { get; set; }
        [MaxLength(20)]
        public string Zap2ItId { get; set; }

        public string ShowName { get; set; }
        public string Network { get; set; }
        public string Overview { get; set; }
        public int? Runtime { get; set; }

        public DateTime? OriginalAirDate { get; set; }
        public virtual ICollection<TvEpisode> Episodes { get; set; }

        public TvShow()
        {
            Episodes = new HashSet<TvEpisode>();
        }
    }
}
