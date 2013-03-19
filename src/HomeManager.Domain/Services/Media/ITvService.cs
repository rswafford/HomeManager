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
        TvEpisode GetTvEpisode(string thumbprint);
        OperationResult<TvEpisode> AddTvEpisode(TvEpisode episode);
        TvEpisode UpdateTvEpisode(TvEpisode episode);

        OperationResult<UserTvEpisode> AddUserTvEpisode(UserTvEpisode userEpisode);

        TvShow GetTvShow(Guid key);
        TvShow GetTvShow(string showName);
        OperationResult<TvShow> AddShow(TvShow show);
        TvShow UpdateTvShow(TvShow show);
    }
}
