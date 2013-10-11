using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Dtos
{
    public class TvEpisodeDto
    {
        public Guid Key { get; set; }
        public Guid OwnerKey { get; set; }
        public Guid TvShowKey { get; set; }

        public string Filename { get; set; }
        public string FullPath { get; set; }

        public int Season { get; set; }
        public int Episode { get; set; }

        public string ShowName { get; set; }
    }
}
