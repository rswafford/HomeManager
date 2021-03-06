﻿using System;
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

        public int? Year { get; set; }

        public DateTime? ReleaseDate { get; set; }
        public decimal Rating { get; set; }

        public string Outline { get; set; }
        public string Plot { get; set; }
        public string Tagline { get; set; }

        public int? Runtime { get; set; }
        public int? Top250 { get; set; }

        public Guid? FormatKey { get; set; }
        public MovieFormat Format { get; set; }

        public virtual ICollection<MovieInGenre> MovieInGenres { get; set; }
        public virtual ICollection<UserMovie> UserMovies { get; set; }

        public Movie() : base()
        {
            MovieInGenres = new HashSet<MovieInGenre>();
            UserMovies = new HashSet<UserMovie>();
        }
    }
}
