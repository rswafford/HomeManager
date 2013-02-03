using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Domain.Entities.Core;
using HomeManager.Domain.Entities.Media;

namespace HomeManager.Domain.Entities.Extensions
{
    public static class MovieFormatRepositoryExtensions
    {
        public static MovieFormat GetSingleByAbbreviation(this IEntityRepository<MovieFormat> movieFormatRepository, string abbreviation)
        {
            var abbreviations = abbreviation.Split(',');
            return
                abbreviations.Select(abbr => string.Format(",{0},", abbr))
                             .Select(searchString => movieFormatRepository.FindBy(x => x.Abbreviation.Contains(searchString))
                                 .FirstOrDefault()).FirstOrDefault(format => format != null);
        }
    }
}
