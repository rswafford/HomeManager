using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Domain.Entities.Core;
using HomeManager.Domain.Entities.Media;

namespace HomeManager.Domain.Services.Media
{
    public class TvService : ITvService
    {
        private readonly IEntityRepository<TvShow> _tvShowRepository;
        private readonly IEntityRepository<MovieFormat> _movieFormatRepository;
        private readonly IEntityRepository<TvEpisode> _tvEpisodeRepository;
        private readonly IEntityRepository<UserTvEpisode> _userTvEpisodeRepository;

        public TvService(IEntityRepository<TvShow> tvShowRepository,
            IEntityRepository<MovieFormat> movieFormatRepository,
            IEntityRepository<TvEpisode> tvEpisodeRepository,
            IEntityRepository<UserTvEpisode> userTvEpisodeRepository)
        {
            _tvShowRepository = tvShowRepository;
            _movieFormatRepository = movieFormatRepository;
            _tvEpisodeRepository = tvEpisodeRepository;
            _userTvEpisodeRepository = userTvEpisodeRepository;
        }

        public TvEpisode GetTvEpisode(Guid key)
        {
            var tvEp = _tvEpisodeRepository.GetSingle(key);
            return tvEp;
        }

        public TvEpisode GetTvEpisode(string thumbprint, int? season, int? episode)
        {
            var tvEp =
                _tvEpisodeRepository.FindBy(
                    t =>
                    t.FileHash == thumbprint && (!episode.HasValue || t.Episode == episode.Value) &&
                    (!season.HasValue || t.Season == season.Value));
            if (!tvEp.Any())
                return null;

            return tvEp.First();
        }

        public IEnumerable<TvEpisode> GetShowEpisodes(Guid showKey)
        {
            return _tvEpisodeRepository.FindBy(t => t.TvShowKey == showKey);
        }

        public IEnumerable<UserTvEpisode> GetUserTvEpisodes(Guid userKey)
        {
            return _userTvEpisodeRepository.AllIncluding(t => t.TvEpisode, t => t.TvEpisode.TvShow, t => t.Owner).Where(t => t.OwnerKey == userKey);
        }

        public OperationResult<TvEpisode> AddTvEpisode(TvEpisode episode)
        {
            if (_tvEpisodeRepository.FindBy(m => m.FileHash == episode.FileHash && m.Episode == episode.Episode && m.Season == episode.Season).Any())
            {
                return new OperationResult<TvEpisode>(false);
            }
            
            episode.Key = Guid.NewGuid();
            _tvEpisodeRepository.Add(episode);
            _tvEpisodeRepository.Save();

            return new OperationResult<TvEpisode>(true) { Entity = episode };
        }

        public TvEpisode UpdateTvEpisode(TvEpisode episode)
        {
            _tvEpisodeRepository.Edit(episode);
            _tvEpisodeRepository.Save();

            return episode;
        }

        public int CountUserTvEpisodes(Guid userKey)
        {
            return _userTvEpisodeRepository.FindBy(t => t.OwnerKey == userKey).Count();
        }

        public UserTvEpisode GetUserTvEpisode(Guid tvEpisodeKey, Guid userKey)
        {
            var userEpisode =
                _userTvEpisodeRepository.FindBy(utv => utv.OwnerKey == userKey && utv.TvEpisodeKey == tvEpisodeKey);
            if (userEpisode.Any())
                return userEpisode.First();

            return null;
        }

        public OperationResult<UserTvEpisode> AddUserTvEpisode(UserTvEpisode userEpisode)
        {
            if(GetUserTvEpisode(userEpisode.TvEpisodeKey, userEpisode.OwnerKey) != null)
            {
                return new OperationResult<UserTvEpisode>(false);
            }

            userEpisode.Key = Guid.NewGuid();
            _userTvEpisodeRepository.Add(userEpisode);
            _userTvEpisodeRepository.Save();

            return new OperationResult<UserTvEpisode>(true) {Entity = userEpisode};
        }

        public UserTvEpisode UpdateUserTvEpisode(UserTvEpisode userEpisode)
        {
            _userTvEpisodeRepository.Edit(userEpisode);
            _userTvEpisodeRepository.Save();

            return userEpisode;
        }

        public TvShow GetTvShow(Guid key)
        {
            var tvShow = _tvShowRepository.GetSingle(key);
            return tvShow;
        }

        public TvShow GetTvShow(string showName)
        {
            var tvShow = _tvShowRepository.FindBy(t => t.ShowName == showName);
            if (!tvShow.Any())
                return null;

            return tvShow.First();
        }

        public OperationResult<TvShow> AddShow(TvShow show)
        {
            var tvShow = _tvShowRepository.FindBy(t => t.ShowName == show.ShowName);
            if(tvShow.Any())
            {
                return new OperationResult<TvShow>(false);
            }

            show.Key = Guid.NewGuid();
            _tvShowRepository.Add(show);
            _tvShowRepository.Save();

            return new OperationResult<TvShow>(true) { Entity = show };
        }

        public TvShow UpdateTvShow(TvShow show)
        {
            _tvShowRepository.Edit(show);
            _tvShowRepository.Save();

            return show;
        }

        public int CountUserTvShows(Guid userKey)
        {
            return
                _userTvEpisodeRepository.FindBy(t => t.OwnerKey == userKey)
                                        .Select(t => t.TvEpisode.TvShowKey)
                                        .Distinct()
                                        .Count();
        }

        public IEnumerable<TvShow> GetUserTvShows(Guid userKey)
        {
            //var eps = GetUserTvEpisodes(userKey);

            var ctx = new EntitiesContext();
            var shows = (from s in ctx.UserTvEpisodes
                         where s.OwnerKey == userKey
                         select s.TvEpisode.TvShow);

            return shows.Distinct();
        }

        public int CountTvShowEpisodes(Guid show, Guid user)
        {
            return _userTvEpisodeRepository.FindBy(t => t.OwnerKey == user && t.TvEpisode.TvShowKey == show).Count();
        }
    }
}
