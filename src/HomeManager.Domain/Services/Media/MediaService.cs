using System;
using HomeManager.Domain.Entities.Core;
using HomeManager.Domain.Entities.Extensions;
using HomeManager.Domain.Entities.Media;

namespace HomeManager.Domain.Services.Media
{
    public class MediaService : IMediaService
    {
        private readonly IEntityRepository<Movie> _movieRepository;
        private readonly IEntityRepository<MovieGenre> _movieGenreRepository;
        private readonly IEntityRepository<MovieFormat> _movieFormatRepository;
        private readonly IEntityRepository<MovieInGenre> _movieInGenreRepository;

        public MediaService(IEntityRepository<Movie> movieRepository,
            IEntityRepository<MovieGenre> movieGenreRepository,
            IEntityRepository<MovieFormat> movieFormatRepository,
            IEntityRepository<MovieInGenre> movieInGenreRepository)
        {
            _movieRepository = movieRepository;
            _movieGenreRepository = movieGenreRepository;
            _movieFormatRepository = movieFormatRepository;
            _movieInGenreRepository = movieInGenreRepository;
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
            return _movieRepository.Paginate(pageIndex, pageSize, x => x.Title, y => y.OwnerKey == ownerKey);
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

        public OperationResult<Movie> AddMovie(Movie movie)
        {
            if(_movieRepository.FindMovieByOwner(movie) != null)
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
    }
}
