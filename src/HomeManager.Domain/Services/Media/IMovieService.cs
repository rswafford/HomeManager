﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Domain.Entities.Core;
using HomeManager.Domain.Entities.Media;

namespace HomeManager.Domain.Services.Media
{
    public interface IMovieService
    {
        // Movie Methods
        PaginatedList<MovieGenre> GetMovieGenres(int pageIndex, int pageSize);
        MovieGenre GetMovieGenre(Guid key);
        OperationResult<MovieGenre> AddMovieGenre(MovieGenre movieGenre);
        MovieGenre UpdateMovieGenre(MovieGenre movieGenre);

        PaginatedList<MovieFormat> GetMovieFormats(int pageIndex, int pageSize);
        MovieFormat GetMovieFormat(Guid key);
        PaginatedList<MovieFormat> FindMovieFormat(int pageIndex, int pageSize, string abbreviation);
        OperationResult<MovieFormat> AddMovieFormat(MovieFormat movieFormat);
        MovieFormat UpdateMovieFormat(MovieFormat movieFormat);

        PaginatedList<Movie> GetMovies(int pageIndex, int pageSize);
        PaginatedList<Movie> GetMovies(int pageIndex, int pageSize, Guid ownerKey);
        PaginatedList<Movie> SearchMovies(int pageIndex, int pageSize, string searchTerm);
        PaginatedList<Movie> SearchMovies(int pageIndex, int pageSize, string searchTerm, Guid ownerKey);
        Movie GetMovie(Guid key);
        Movie GetMovie(string thumbprint);
        OperationResult<Movie> AddMovie(Movie movie);
        Movie UpdateMovie(Movie movie);

        int CountUserMovies(Guid userKey);
        UserMovie GetUserMovie(Guid movieKey, Guid userKey);
        IEnumerable<UserMovie> GetUserMovies(Guid userKey);
        OperationResult<UserMovie> AddUserMovie(UserMovie userMovie);
        UserMovie UpdateUserMovie(UserMovie userMovie);
    }
}
