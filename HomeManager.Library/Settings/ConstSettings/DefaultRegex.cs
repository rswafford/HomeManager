using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Library.Settings.ConstSettings
{
    public static class DefaultRegex
    {
        /// <summary>
        /// Extract an hour int and a minute int.
        /// </summary>
        public const string HourMinute = @"(?<Hour>\d*)h\s(?<Minute>\d*)\w\w";

        /// <summary>
        /// Used to detect if a filename is in a TV show format.
        /// </summary>
        public const string Tv = @"(?<![0-9])((s[0-9]{1,4})|[0-9]{1,2})(?:(\s|\.|x))?((?:(e|x)\s?[0-9]+)+)";

        /// <summary>
        /// Used to extract season number.
        /// </summary>
        public const string TvSeason = @"s{0,1}([0-9]+)(\\s|\\.)?[ex-]";

        /// <summary>
        /// Used to extract tv episodes.
        /// </summary>
        public const string TvEpisode = @"(?!x264)([EXex]\s?([0-9]+)|(([0-9]{4}-[0-9]{2}(-[0-9]{2})?)|([0-9]{2}-[0-9]{2}-[0-9]{4})|(?!720p)((?<season>[0-9]+)(-?[0-9]{2,})+(?![0-9]))))(?<!x264)";
    }
}
