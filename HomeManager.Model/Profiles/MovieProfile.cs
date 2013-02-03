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

            Mapper.CreateMap<MovieImportRequestModel, Movie>()
                .ForMember(dest => dest.ImportedFormatString, opts => opts.MapFrom(src => src.Format))
                .ForMember(dest => dest.Format, opts => opts.Ignore());
        }
    }
}
