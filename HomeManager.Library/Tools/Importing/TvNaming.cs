using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HomeManager.Library.Settings.ConstSettings;
using HomeManager.Library.Tools.Extensions;
using log4net;

namespace HomeManager.Library.Tools.Importing
{
    public static class TvNaming
    {
        private static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static string ExtractSeriesName(string filePath)
        {
            string finalSeriesName = string.Empty;
            string series = Path.GetFileNameWithoutExtension(filePath);
            
            // Check if dir structure is <root>/<series>/<season>/<episodes>
            string dir = Directory.GetParent(filePath).Name;
            string seriesGuess = string.Empty;
            if (dir.StartsWith("season", true, System.Globalization.CultureInfo.CurrentCulture))
            {
                // Looks like it. Qualified series guess
                seriesGuess = Directory.GetParent(filePath).Parent.Name;
                finalSeriesName = seriesGuess;
            }
            
            var regex = Regex.Match(series, "(?<seriesName>.*?)" + DefaultRegex.Tv, RegexOptions.IgnoreCase);

            if (regex.Success)
            {
                string rawSeriesName = regex.Groups["seriesName"].Value.Trim();
                Log.DebugFormat("Series name from file: {0}", rawSeriesName);
                
                string rawSeriesGuess = rawSeriesName.Replace(new string[] {".", "_", "-"}, " ").Trim();

                if (seriesGuess != string.Empty)
                {
                    if (rawSeriesName == string.Empty)
                    {
                        // Anything's better than nothing
                        rawSeriesName = seriesGuess;
                    }
                    else if (rawSeriesName.StartsWith(seriesGuess))
                    {
                        // Regex might've picked up some random crap between series name and s01e01
                        rawSeriesName = seriesGuess;
                    }
                }
                else
                {
                    rawSeriesName = rawSeriesGuess;
                }

                Log.DebugFormat("Best series guess: {0}", rawSeriesName);

                finalSeriesName = rawSeriesName;
            }

            return finalSeriesName;
        }

        public static int ExtractSeasonNumber(string filePath)
        {
            int retVal = -1;
            string series = Path.GetFileNameWithoutExtension(filePath);
            var seasonMatch = Regex.Match(series, DefaultRegex.TvSeason, RegexOptions.IgnoreCase);
            if (seasonMatch.Success)
            {
                retVal = seasonMatch.Groups[1].Value.GetNumber();
            }

            return retVal;
        }

        public static IEnumerable<int> ExtractEpisodeNumber(string filePath)
        {
            List<int> episodeNumbers = new List<int>();

            string series = Path.GetFileNameWithoutExtension(filePath);
            var episodeMatch = Regex.Matches(series, DefaultRegex.TvEpisode, RegexOptions.IgnoreCase);
            if (episodeMatch.Count > 0)
            {
                if (episodeMatch.Count > 1)
                {
                    bool first = true;
                    foreach (Match match in episodeMatch)
                    {
                        episodeNumbers.Add(match.Value.GetNumber());
                    }

                    Log.DebugFormat("Extracted episode numbers ({0}): {1}", episodeNumbers.Count,
                                    string.Join(", ", episodeNumbers));
                }
                else
                {
                    var episodeMatch2 = Regex.Match(series, DefaultRegex.TvEpisode, RegexOptions.IgnoreCase);
                    episodeNumbers.Add(episodeMatch2.Groups[1].Value.GetNumber());

                    Log.DebugFormat("Extracted episode number: {0}", episodeNumbers.First());
                }
            }

            return episodeNumbers;
        }
    }
}
