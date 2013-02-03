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
    public class MovieInGenre : IEntity
    {
        [Key]
        public Guid Key { get; set; }
        public Guid MovieKey { get; set; }
        public Guid GenreKey { get; set; }

        public Movie Movie { get; set; }
        public MovieGenre Genre { get; set; }
    }
}
