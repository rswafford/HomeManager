using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Dtos
{
    public class MovieDto
    {
        public Guid Key { get; set; }
        public Guid OwnerKey { get; set; }

        public string Filename { get; set; }
        public string FullPath { get; set; }

        public string Title { get; set; }
        public string ImdbId { get; set; }
        public string MovieDbId { get; set; }
        public string Year { get; set; }

        public DateTime ReleaseDate { get; set; }
        public decimal Rating { get; set; }

        public string Outline { get; set; }
        public string Plot { get; set; }

        public Guid FormatKey { get; set; }
    }
}
