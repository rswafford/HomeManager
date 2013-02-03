using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Domain.Entities.Core;

namespace HomeManager.Domain.Entities.Media
{
    public class MovieGenre : IEntity
    {
        [Key]
        public Guid Key { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MovieInGenre> MoviesInGenre { get; set; }

        public MovieGenre()
        {
            MoviesInGenre = new HashSet<MovieInGenre>();
        }
    }
}
