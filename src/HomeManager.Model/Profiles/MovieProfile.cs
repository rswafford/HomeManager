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
    public class MovieProfile : Profile
    {
        public override string ProfileName
        {
            get { return "MovieProfile"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<MovieDto, Movie>();

            Mapper.CreateMap<UserMovie, MovieDto>()
                .ForMember(d => d.Key, o => o.MapFrom(s => s.MovieKey))
                .ForMember(d => d.OwnerKey, o => o.MapFrom(s => s.OwnerKey))
                .ForMember(d => d.MovieDbId, o => o.MapFrom(s => s.Movie.MovieDbId))
                .ForMember(d => d.Outline, o => o.MapFrom(s => s.Movie.Outline))
                .ForMember(d => d.Plot, o => o.MapFrom(s => s.Movie.Plot))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Movie.Title))
                .ForMember(d => d.ImdbId, o=> o.MapFrom(s => s.Movie.ImdbId));

            Mapper.CreateMap<MovieImportRequestModel, Movie>()
                .ForMember(dest => dest.Format, opts => opts.Ignore());

            Mapper.CreateMap<MovieImportRequestModel, UserMovie>()
                .ForMember(dest => dest.ImportedFormatString, opts => opts.MapFrom(src => src.Format));
        }
    }
}
