using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Domain.Entities.Media;

namespace HomeManager.Domain.Services.Media
{
    public interface ITvService
    {
        TvEpisode GetTvEpisode(Guid key);
        TvEpisode GetTvEpisode(string thumbprint, int? season, int? episode);
        IEnumerable<TvEpisode> GetShowEpisodes(Guid showKey);
            
        OperationResult<TvEpisode> AddTvEpisode(TvEpisode episode);
        TvEpisode UpdateTvEpisode(TvEpisode episode);

        int CountUserTvEpisodes(Guid userKey);
        UserTvEpisode GetUserTvEpisode(Guid tvEpisodeKey, Guid userKey);
        IEnumerable<UserTvEpisode> GetUserTvEpisodes(Guid userKey);
        OperationResult<UserTvEpisode> AddUserTvEpisode(UserTvEpisode userEpisode);
        UserTvEpisode UpdateUserTvEpisode(UserTvEpisode userEpisode);

        TvShow GetTvShow(Guid key);
        TvShow GetTvShow(string showName);
        OperationResult<TvShow> AddShow(TvShow show);
        TvShow UpdateTvShow(TvShow show);
        int CountUserTvShows(Guid key);
        IEnumerable<TvShow> GetUserTvShows(Guid userKey);
        int CountTvShowEpisodes(Guid show, Guid user);
    }
}
