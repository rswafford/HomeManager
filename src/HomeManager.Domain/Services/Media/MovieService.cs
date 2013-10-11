using System;
using System.Collections.Generic;
using System.Linq;
using HomeManager.Domain.Entities.Core;
using HomeManager.Domain.Entities.Extensions;
using HomeManager.Domain.Entities.Media;

namespace HomeManager.Domain.Services.Media
{
    public class MovieService : IMovieService
    {
        private readonly IEntityRepository<Movie> _movieRepository;
        private readonly IEntityRepository<MovieGenre> _movieGenreRepository;
        private readonly IEntityRepository<MovieFormat> _movieFormatRepository;
        private readonly IEntityRepository<MovieInGenre> _movieInGenreRepository;
        private readonly IEntityRepository<UserMovie> _userMovieRepository;

        public MovieService(IEntityRepository<Movie> movieRepository,
            IEntityRepository<MovieGenre> movieGenreRepository,
            IEntityRepository<MovieFormat> movieFormatRepository,
            IEntityRepository<MovieInGenre> movieInGenreRepository, 
            IEntityRepository<UserMovie> userMovieRepository)
        {
            _movieRepository = movieRepository;
            _movieGenreRepository = movieGenreRepository;
            _movieFormatRepository = movieFormatRepository;
            _movieInGenreRepository = movieInGenreRepository;
            _userMovieRepository = userMovieRepository;
        }

        public PaginatedList<MovieGenre> GetMovieGenres(int pageIndex, int pageSize)
        {
            var movieGenres = _movieGenreRepository.Paginate(pageIndex, pageSize, x => x.Name);
            return movieGenres;
        }

        public MovieGenre GetMovieGenre(Guid key)
        {
            var movieGenre = _movieGenreRepository.GetSingle(key);
            return movieGenre;
        }

        public OperationResult<MovieGenre> AddMovieGenre(MovieGenre movieGenre)
        {
            if(_movieGenreRepository.GetSingleByName(movieGenre.Name) != null)
            {
                return new OperationResult<MovieGenre>(false);
            }

            movieGenre.Key = Guid.NewGuid();
            _movieGenreRepository.Add(movieGenre);
            _movieGenreRepository.Save();

            return new OperationResult<MovieGenre>(true) {Entity = movieGenre};
        }

        public MovieGenre UpdateMovieGenre(MovieGenre movieGenre)
        {
            _movieGenreRepository.Edit(movieGenre);
            _movieGenreRepository.Save();

            return movieGenre;
        }

        public PaginatedList<MovieFormat> GetMovieFormats(int pageIndex, int pageSize)
        {
            var movieFormats = _movieFormatRepository.Paginate(pageIndex, pageSize, x => x.Description);
            return movieFormats;
        }

        public MovieFormat GetMovieFormat(Guid key)
        {
            var movieFormat = _movieFormatRepository.GetSingle(key);
            return movieFormat;
        }

        public PaginatedList<MovieFormat> FindMovieFormat(int pageIndex, int pageSize, string abbreviation)
        {
            var movieFormats =
                _movieFormatRepository.Paginate(pageIndex, pageSize, x => x.Description,
                                                y => y.Abbreviation.Contains(abbreviation));

            return movieFormats;
        }

        public OperationResult<MovieFormat> AddMovieFormat(MovieFormat movieFormat)
        {
            if (_movieFormatRepository.GetSingleByAbbreviation(movieFormat.Abbreviation) != null)
            {
                return new OperationResult<MovieFormat>(false);
            }

            movieFormat.Key = Guid.NewGuid();
            _movieFormatRepository.Add(movieFormat);
            _movieFormatRepository.Save();

            return new OperationResult<MovieFormat>(true) { Entity = movieFormat };
        }

        public MovieFormat UpdateMovieFormat(MovieFormat movieFormat)
        {
            _movieFormatRepository.Edit(movieFormat);
            _movieGenreRepository.Save();

            return movieFormat;
        }

        public PaginatedList<Movie> GetMovies(int pageIndex, int pageSize)
        {
            return _movieRepository.Paginate(pageIndex, pageSize, x => x.Title);
        }

        public PaginatedList<Movie> GetMovies(int pageIndex, int pageSize, Guid ownerKey)
        {
            return _movieRepository.Paginate(pageIndex, pageSize, x => x.Title, y => y.UserMovies.Any(m => m.Owner.Key == ownerKey));
        }

        public PaginatedList<Movie> SearchMovies(int pageIndex, int pageSize, string searchTerm)
        {
            return _movieRepository.Search(pageIndex, pageSize, searchTerm);
        }

        public PaginatedList<Movie> SearchMovies(int pageIndex, int pageSize, string searchTerm, Guid ownerKey)
        {
            return _movieRepository.SearchByOwner(pageIndex, pageSize, searchTerm, ownerKey);
        }

        public Movie GetMovie(Guid key)
        {
            return _movieRepository.GetSingle(key);
        }

        public Movie GetMovie(string thumbprint)
        {
            var movies = _movieRepository.FindBy(m => m.FileHash == thumbprint);
            if (!movies.Any())
                return null;

            return movies.First();
        }

        public OperationResult<Movie> AddMovie(Movie movie)
        {
            if(_movieRepository.FindBy(m => m.FileHash == movie.FileHash).Any())
            {
                return new OperationResult<Movie>(false);
            }

            movie.Key = Guid.NewGuid();
            _movieRepository.Add(movie);
            _movieRepository.Save();

            return new OperationResult<Movie>(true) {Entity = movie};
        }

        public Movie UpdateMovie(Movie movie)
        {
            _movieRepository.Edit(movie);
            _movieRepository.Save();

            return movie;
        }

        public int CountUserMovies(Guid userKey)
        {
            return _userMovieRepository.FindBy(m => m.OwnerKey == userKey).Count();
        }

        public UserMovie GetUserMovie(Guid movieKey, Guid userKey)
        {
            var userMovie = _userMovieRepository.FindBy(m => m.OwnerKey == userKey && m.MovieKey == movieKey);

            if (userMovie.Any())
                return userMovie.First();

            return null;
        }

        public IEnumerable<UserMovie> GetUserMovies(Guid userKey)
        {
            return _userMovieRepository.AllIncluding(m => m.Movie, m => m.Owner).Where(m => m.OwnerKey == userKey);
        }

        public OperationResult<UserMovie> AddUserMovie(UserMovie userMovie)
        {
            if(GetUserMovie(userMovie.MovieKey, userMovie.OwnerKey) != null)
            {
                return new OperationResult<UserMovie>(false);
            }

            userMovie.Key = Guid.NewGuid();
            _userMovieRepository.Add(userMovie);
            _userMovieRepository.Save();

            return new OperationResult<UserMovie>(true) {Entity = userMovie};
        }

        public UserMovie UpdateUserMovie(UserMovie userMovie)
        {
            _userMovieRepository.Edit(userMovie);
            _userMovieRepository.Save();

            return userMovie;
        }
    }
}
