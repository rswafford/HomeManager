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

        public TvEpisode GetTvEpisode(string thumbprint)
        {
            var tvEp = _tvEpisodeRepository.FindBy(t => t.FileHash == thumbprint);
            if (!tvEp.Any())
                return null;

            return tvEp.First();
        }

        public OperationResult<TvEpisode> AddTvEpisode(TvEpisode episode)
        {
            if (_tvEpisodeRepository.FindBy(m => m.FileHash == episode.FileHash).Any())
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

        public OperationResult<UserTvEpisode> AddUserTvEpisode(UserTvEpisode userEpisode)
        {
            if(_userTvEpisodeRepository.FindBy(utv => utv.OwnerKey == userEpisode.OwnerKey && utv.TvEpisodeKey == userEpisode.TvEpisodeKey).Any())
            {
                return new OperationResult<UserTvEpisode>(false);
            }

            userEpisode.Key = Guid.NewGuid();
            _userTvEpisodeRepository.Add(userEpisode);
            _userTvEpisodeRepository.Save();

            return new OperationResult<UserTvEpisode>(true) {Entity = userEpisode};
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
    }
}
