using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Dtos
{
    public class TvShowDto
    {
        public Guid Key { get; set; }
        public string ImdbId { get; set; }
        public string TvDbId { get; set; }
        public string Zap2ItId { get; set; }

        public string ShowName { get; set; }
        public string Network { get; set; }
        public string Overview { get; set; }
        public int? Runtime { get; set; }

        public DateTime? OriginalAirDate { get; set; }
        public int EpisodeCount { get; set; }
    }
}
