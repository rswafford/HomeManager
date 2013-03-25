using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
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
    public class TvImportController : ApiController
    {
        private readonly ITvService _tvService;
        private readonly IMembershipService _membershipService;
        private static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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

            TvEpisode dbEp = _tvService.GetTvEpisode(requestModel.FileHash, requestModel.Season, requestModel.Episode);
            if (dbEp == null)
            {
                var tvEpisode = Mapper.Map<TvEpisode>(requestModel);
                tvEpisode.TvShowKey = tvShowKey;
                var createdEp = _tvService.AddTvEpisode(tvEpisode);
                dbEp = createdEp.Entity;
                if (!createdEp.IsSuccess)
                {
                    Log.ErrorFormat("Could not create episode {0} [{1}].  Hashcode was {2}.", string.Format("{0}.S{1}E{2}", requestModel.SeriesName, requestModel.Season, requestModel.Episode), requestModel.FullPath, requestModel.FileHash);
                    return new HttpResponseMessage(HttpStatusCode.Conflict);
                }
            }

            var existingUserEpisode = _tvService.GetUserTvEpisode(dbEp.Key, user.User.Key);

            if (existingUserEpisode == null)
            {
                var userEpisode = Mapper.Map<UserTvEpisode>(requestModel);
                userEpisode.TvEpisodeKey = dbEp.Key;
                userEpisode.OwnerKey = user.User.Key;

                var createdUserEpisode = _tvService.AddUserTvEpisode(userEpisode);
                if (!createdUserEpisode.IsSuccess)
                {
                    Log.ErrorFormat("Could not add episode {0} to user {1}.  Episode ID: {2}",
                                    string.Format("{0}.S{1}E{2}", requestModel.SeriesName, requestModel.Season,
                                                  requestModel.Episode), user.User.Key, userEpisode.TvEpisodeKey);

                    Log.ErrorFormat("Imported File Path: {0}", requestModel.FullPath);
                    Log.ErrorFormat("Imported File Hash: {0}", requestModel.FileHash);

                    var existingUserMovie = dbEp.UserTvEpisodes.FirstOrDefault(x => x.OwnerKey == user.User.Key);
                    if (existingUserMovie != null)
                    {
                        Log.ErrorFormat("Existing File Path: {0}", existingUserMovie.FullPath);
                        Log.ErrorFormat("Existing File Hash: {0}", dbEp.FileHash);
                    }
                    else
                    {
                        Log.Error("No existing episode. BOGUS ERROR!!!!");
                    }

                    return new HttpResponseMessage(HttpStatusCode.Conflict);
                }

                existingUserEpisode = createdUserEpisode.Entity;
            }
            else if(requestModel.UpdateExisting)
            {
                existingUserEpisode.Filename = requestModel.Filename;
                existingUserEpisode.FullPath = requestModel.FullPath;

                _tvService.UpdateUserTvEpisode(existingUserEpisode);
            }

            var response = Request.CreateResponse(HttpStatusCode.Created, Mapper.Map<TvEpisodeDto>(existingUserEpisode));

            return response;
        }
    }
}
