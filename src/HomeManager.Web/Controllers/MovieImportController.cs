using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using HomeManager.Domain.Entities.Media;
using HomeManager.Domain.Services.Media;
using HomeManager.Model.Dtos;
using HomeManager.Model.RequestModels;
using HomeManager.Web.Filters;

namespace HomeManager.Web.Controllers
{
    public class MovieImportController : ApiController
    {
        private readonly IMediaService _mediaService;

        public MovieImportController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        // POST api/movie
        [EmptyParameterFilter("requestModel")]
        public HttpResponseMessage Post(MovieImportRequestModel requestModel)
        {
            var movie = Mapper.Map<Movie>(requestModel);
            var createdMovie = _mediaService.AddMovie(movie);
            if(!createdMovie.IsSuccess)
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict);
            }

            var response = Request.CreateResponse(HttpStatusCode.Created, Mapper.Map<MovieDto>(createdMovie.Entity));

            //response.Headers.Location = new Uri(Url.Link(routeName, new {key = createdMovie.Entity.Key}));

            return response;
        }
    }
}
