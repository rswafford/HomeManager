﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Domain.Entities.Media
{
    public class TvEpisode : MediaItem
    {
        public Guid? TvShowKey { get; set; }
        public int? Season { get; set; }
        public int? Episode { get; set; }

        public DateTime? OriginalAirDate { get; set; }
        
        public Guid? FormatKey { get; set; }
        public MovieFormat Format { get; set; }

        public TvShow TvShow { get; set; }

        public virtual ICollection<UserTvEpisode> UserTvEpisodes { get; set; }

        public TvEpisode()
            : base()
        {
            UserTvEpisodes = new HashSet<UserTvEpisode>();
        }
    }
}
