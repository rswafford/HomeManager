using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Domain.Entities.Core;
using HomeManager.Domain.Entities.Media;

namespace HomeManager.Domain.Entities.Extensions
{
    public static class MovieRepositoryExtensions
    {
        public static Movie FindMovieByOwner(this IEntityRepository<Movie> movieRepository, Movie movie, Guid ownerKey)
        {
            var userMovies = movieRepository.FindBy(m => m.UserMovies.Any(u => u.OwnerKey == ownerKey));
            
            var matches = userMovies.Where(um => um.FileHash == movie.FileHash);

            return matches.FirstOrDefault();
        }

        public static PaginatedList<Movie> Search(this IEntityRepository<Movie> movieRepository, int pageIndex, int pageSize, string searchString)
        {
            return movieRepository.Paginate(pageIndex, pageSize, x => x.Title,
                                            movie =>
                                            movie.Title.Contains(searchString) || 
                                            movie.Year.Equals(searchString));
        }

        public static PaginatedList<Movie> SearchByOwner(this IEntityRepository<Movie> movieRepository, int pageIndex, int pageSize, string searchString, Guid ownerKey)
        {
            return movieRepository.Paginate(pageIndex, pageSize, x => x.Title,
                                            movie =>
                                            movie.UserMovies.Any(u => u.OwnerKey == ownerKey) &&
                                            (movie.Title.Contains(searchString) ||
                                             movie.Year.Equals(searchString)));
        }
    }
}
