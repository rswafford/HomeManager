using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HomeManager.Domain.Entities.Media;
using HomeManager.Model.Dtos;
using HomeManager.Model.RequestModels;

namespace HomeManager.Model.Profiles
{
    public class TvProfile : Profile
    {
        public override string ProfileName
        {
            get { return "TvProfile"; }
        }

        protected override void Configure()
        {
            CreateMap<UserTvEpisode, TvEpisodeDto>()
                .ForMember(d => d.Season, o => o.MapFrom(s => s.TvEpisode.Season))
                .ForMember(d => d.Episode, o => o.MapFrom(s => s.TvEpisode.Episode))
                .ForMember(d => d.TvShowKey, o => o.MapFrom(s => s.TvEpisode.TvShowKey))
                .ForMember(d => d.ShowName, o => o.MapFrom(s => s.TvEpisode.TvShow.ShowName));

            CreateMap<TvShow, TvShowDto>();

            CreateMap<TvShowImportRequestModel, TvEpisode>()
                .ForMember(dest => dest.Format, opts => opts.Ignore());

            CreateMap<TvShowImportRequestModel, UserTvEpisode>()
                .ForMember(dest => dest.ImportedFormatString, opts => opts.MapFrom(src => src.Format));
        }
    }
}
