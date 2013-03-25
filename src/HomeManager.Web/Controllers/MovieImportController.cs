using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using AutoMapper;
using HomeManager.Domain.Entities.Media;
using HomeManager.Domain.Services.Media;
using HomeManager.Domain.Services.Membership;
using HomeManager.Model.Dtos;
using HomeManager.Model.RequestModels;
using HomeManager.Web.Filters;
using log4net;

namespace HomeManager.Web.Controllers
{
    [Authorize]
    public class MovieImportController : ApiController
    {
        private readonly IMovieService _movieService;
        private readonly IMembershipService _membershipService;
        private static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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
                    Log.ErrorFormat("Could not create movie {0} [{1}].  Hashcode was {2}.", requestModel.Title, requestModel.FullPath, requestModel.FileHash);
                    return new HttpResponseMessage(HttpStatusCode.Conflict);
                }
                dbMovie = createdMovie.Entity;
            }

            var dbUserMovie = _movieService.GetUserMovie(dbMovie.Key, user.User.Key);

            if (dbUserMovie == null)
            {
                var userMovie = Mapper.Map<UserMovie>(requestModel);
                userMovie.OwnerKey = user.User.Key;
                userMovie.MovieKey = dbMovie.Key;

                var created = _movieService.AddUserMovie(userMovie);
                if (!created.IsSuccess)
                {
                    Log.ErrorFormat("Could not add movie {0} to user {1}.  Movie ID: {2}", requestModel.Title,
                                    user.User.Key, userMovie.MovieKey);
                    Log.ErrorFormat("Imported File Path: {0}", requestModel.FullPath);
                    Log.ErrorFormat("Imported File Hash: {0}", requestModel.FileHash);

                    var existingUserMovie = dbMovie.UserMovies.FirstOrDefault(x => x.OwnerKey == user.User.Key);
                    if (existingUserMovie != null)
                    {
                        Log.ErrorFormat("Existing File Path: {0}", existingUserMovie.FullPath);
                        Log.ErrorFormat("Existing File Hash: {0}", dbMovie.FileHash);
                    }
                    else
                    {
                        Log.Error("No existing movie. BOGUS ERROR!!!!");
                    }
                    return new HttpResponseMessage(HttpStatusCode.Conflict);
                }
                dbUserMovie = created.Entity;
            }
            else if (requestModel.UpdateExisting)
            {
                dbUserMovie.Filename = requestModel.Filename;
                dbUserMovie.FullPath = requestModel.FullPath;

                _movieService.UpdateUserMovie(dbUserMovie);
            }

            var response = Request.CreateResponse(HttpStatusCode.Created, Mapper.Map<MovieDto>(dbUserMovie));

            return response;
        }
    }
}
