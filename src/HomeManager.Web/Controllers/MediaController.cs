using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeManager.Domain.Services.Media;
using HomeManager.Domain.Services.Membership;
using HomeManager.Model.Web.Media;

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

    }
}
