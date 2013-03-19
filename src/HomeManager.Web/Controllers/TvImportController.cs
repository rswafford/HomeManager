using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    public class TvImportController : ApiController
    {
        private readonly ITvService _tvService;
        private readonly IMembershipService _membershipService;

        public TvImportController(ITvService tvService, IMembershipService membershipService)
        {
            _tvService = tvService;
            _membershipService = membershipService;
        }

        [EmptyParameterFilter("requestModel")]
        public HttpResponseMessage PostTv(TvShowImportRequestModel requestModel)
        {
            string username = User.Identity.Name;
            var user = _membershipService.GetUser(username);

            Guid tvShowKey = Guid.Empty;
            if (!string.IsNullOrEmpty(requestModel.SeriesName))
            {
                Guid showId = Guid.Empty;
                var tvShow = _tvService.GetTvShow(requestModel.SeriesName);
                if (tvShow == null)
                {
                    TvShow show = new TvShow { ShowName = requestModel.SeriesName };
                    var showResult = _tvService.AddShow(show);
                    showId = showResult.Entity.Key;
                }
                else
                {
                    showId = tvShow.Key;
                }
                tvShowKey = showId;
            }

            TvEpisode dbEp = _tvService.GetTvEpisode(requestModel.FileHash);
            if (dbEp == null)
            {
                var tvEpisode = Mapper.Map<TvEpisode>(requestModel);
                tvEpisode.TvShowKey = tvShowKey;
                var createdEp = _tvService.AddTvEpisode(tvEpisode);
                dbEp = createdEp.Entity;
                if (!createdEp.IsSuccess)
                {
                    return new HttpResponseMessage(HttpStatusCode.Conflict);
                }
            }

            var userEpisode = Mapper.Map<UserTvEpisode>(requestModel);
            userEpisode.TvEpisodeKey = dbEp.Key;
            userEpisode.OwnerKey = user.User.Key;

            var createdUserEpisode = _tvService.AddUserTvEpisode(userEpisode);
            var response = Request.CreateResponse(HttpStatusCode.Created, Mapper.Map<TvEpisodeDto>(createdUserEpisode.Entity));

            return response;
        }
    }
}
