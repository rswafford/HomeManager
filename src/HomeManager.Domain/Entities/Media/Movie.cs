using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Domain.Entities.Media
{
    public class Movie : MediaItem
    {
        [MaxLength(10)]
        public string ImdbId { get; set; }
        [MaxLength(10)]
        public string MovieDbId { get; set; }
        [MaxLength(4)]
        public string Year { get; set; }

        public DateTime? ReleaseDate { get; set; }
        public decimal Rating { get; set; }

        public string Outline { get; set; }
        public string Plot { get; set; }

        public Guid? FormatKey { get; set; }
        public MovieFormat Format { get; set; }

        [MaxLength(30)]
        public string ImportedFormatString { get; set; }

        public virtual ICollection<MovieInGenre> MovieInGenres { get; set; }

        public Movie()
        {
            MovieInGenres = new HashSet<MovieInGenre>();
        }
    }
}
