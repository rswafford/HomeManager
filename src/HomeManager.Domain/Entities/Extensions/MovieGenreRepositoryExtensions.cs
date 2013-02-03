using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Domain.Entities.Core;
using HomeManager.Domain.Entities.Media;

namespace HomeManager.Domain.Entities.Extensions
{
    public static class MovieGenreRepositoryExtensions
    {
        public static MovieGenre GetSingleByName(this IEntityRepository<MovieGenre> movieGenreRepository, string name)
        {
            return movieGenreRepository.FindBy(x => x.Name == name).FirstOrDefault();
        }
    }
}
