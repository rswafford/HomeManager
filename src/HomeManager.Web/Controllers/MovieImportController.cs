using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper;
using HomeManager.Domain.Entities.Media;
using HomeManager.Domain.Services.Media;
using HomeManager.Domain.Services.Membership;
using HomeManager.Model.Dtos;
using HomeManager.Model.RequestModels;
using HomeManager.Web.Filters;

namespace HomeManager.Web.Controllers
{
    [Authorize]
    public class MovieImportController : ApiController
    {
        private readonly IMovieService _movieService;
        private readonly ITvService _tvService;
        private readonly IMembershipService _membershipService;

        public MovieImportController(IMovieService movieService, IMembershipService membershipService)
        {
            _movieService = movieService;
            _membershipService = membershipService;
        }

        // POST api/movie
        [EmptyParameterFilter("requestModel")]
        public HttpResponseMessage Post(MovieImportRequestModel requestModel)
        {
            string username = User.Identity.Name;
            var user = _membershipService.GetUser(username);

            var dbMovie = _movieService.GetMovie(requestModel.FileHash);
            if (dbMovie == null)
            {
                var movie = Mapper.Map<Movie>(requestModel);
                var createdMovie = _movieService.AddMovie(movie);
                if (!createdMovie.IsSuccess)
                {
                    return new HttpResponseMessage(HttpStatusCode.Conflict);
                }
                dbMovie = createdMovie.Entity;
            }

            var userMovie = Mapper.Map<UserMovie>(requestModel);
            userMovie.OwnerKey = user.User.Key;
            userMovie.MovieKey = dbMovie.Key;

            var created = _movieService.AddUserMovie(userMovie);
            if(!created.IsSuccess)
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict);
            }

            var response = Request.CreateResponse(HttpStatusCode.Created, Mapper.Map<MovieDto>(created.Entity));

            return response;
        }
    }
}
