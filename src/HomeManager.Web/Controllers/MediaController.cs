using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using HomeManager.Domain.Entities.Core;
using HomeManager.Domain.Services.Media;
using HomeManager.Domain.Services.Membership;
using HomeManager.Model.Dtos;
using HomeManager.Model.Web.Media;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace HomeManager.Web.Controllers
{
    public class MediaController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ITvService _tvService;
        private readonly IMembershipService _membershipService;

        public MediaController(IMovieService movieService, ITvService tvService, IMembershipService membershipService)
        {
            _movieService = movieService;
            _tvService = tvService;
            _membershipService = membershipService;
        }

        //
        // GET: /Media/

        public ActionResult Index()
        {
            var user = _membershipService.GetUser(User.Identity.Name);

            IndexModel model = new IndexModel();
            model.MovieCount = _movieService.CountUserMovies(user.User.Key);
            model.TvEpisodeCount = _tvService.CountUserTvEpisodes(user.User.Key);
            model.TvShowCount = _tvService.CountUserTvShows(user.User.Key);

            return View(model);
        }

        public ActionResult Movies()
        {
            return View();
        }

        public ActionResult TvShows()
        {
            return View();
        }

        public ActionResult Movies_Read([DataSourceRequest] DataSourceRequest request)
        {
            var user = _membershipService.GetUser(User.Identity.Name);
            var movies = _movieService.GetUserMovies(user.User.Key);

            return Json(Mapper.Map<IEnumerable<MovieDto>>(movies).ToDataSourceResult(request));
        }

        public ActionResult TvShows_Read([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result = new DataSourceResult();
            
            var user = _membershipService.GetUser(User.Identity.Name);
            var shows = _tvService.GetUserTvShows(user.User.Key);
            var showModels = Mapper.Map<IEnumerable<TvShowDto>>(shows);
            result = showModels.ToDataSourceResult(request);
            foreach (var d in result.Data)
            {
                ((TvShowDto) d).EpisodeCount = _tvService.CountTvShowEpisodes(((TvShowDto) d).Key, user.User.Key);
            }
            return Json(result);
        }
    }
}
