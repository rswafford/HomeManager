using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HomeManager.Library.Settings;
using HomeManager.Library.Settings.ConstSettings;
using HomeManager.Library.Tools.Enums;

namespace HomeManager.Library.Tools.Importing
{
    public static class DetectType
    {
        #region Constants and Fields

        /// <summary>
        /// The movie regex.
        /// </summary>
        public const string MovieRegex =
            @"(CD\d{1,10})|(CD\s\d{1,10})|(PART\d{1,10})|(PART\s\d{1,10})|(DISC\d{1,10})|(DSIC\s\d{1,10})|(DISK\d{1,10})|(DISK\s\d{1,10})";

        /// <summary>
        /// The tv regex.
        /// </summary>
        public const string TvRegex = DefaultRegex.Tv;

        #endregion

        #region Public Methods

        /// <summary>
        /// The find type.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="containsTv">
        /// The contains tv.
        /// </param>
        /// <param name="containMovies">
        /// The contain movies.
        /// </param>
        /// <returns>
        /// MediaPathFileType enum
        /// </returns>
        public static MediaPathFileType FindType(
            string fileName, bool containsTv, bool containMovies)
        {
            string ext = Path.GetExtension(fileName).ToLower().Replace(".", string.Empty);
            
            if (Get.VideoExtensions.Contains(ext))
            {
                if (containsTv && Regex.IsMatch(fileName, TvRegex, RegexOptions.IgnoreCase))
                {
                    return MediaPathFileType.TV;
                }

                if (containMovies && Regex.IsMatch(fileName, MovieRegex, RegexOptions.IgnoreCase))
                {
                    return MediaPathFileType.Movie;
                }

                if (fileName.ToLower() == "sample")
                {
                    return MediaPathFileType.Sample;
                }

                if (containMovies)
                {
                    return MediaPathFileType.Movie;
                }

                return MediaPathFileType.Video;
            }

            if (ext == "nfo" || ext == "xml")
            {
                return MediaPathFileType.NFO;
            }

            if (ext == ".gif" || ext == ".jpg" || ext == ".png")
            {
                return MediaPathFileType.NFO;
            }

            if (ext == ".mp3")
            {
                return MediaPathFileType.Music;
            }

            return MediaPathFileType.Unknown;
        }

        public static string FindVideoSource(string filePath)
        {
            var videoSourceTypes = Get.Keywords.GetSourcesAsList();
            var fileName = Path.GetFileNameWithoutExtension(filePath);

            fileName = fileName.Replace(".", " ");
            fileName = fileName.Replace("(", " ");
            fileName = fileName.Replace(")", " ");

            foreach (var source in videoSourceTypes)
            {
                if (fileName.ToLower().Contains(" " + source.ToLower() + " ") ||
                    fileName.ToLower().Contains(" " + source.ToLower()))
                {
                    return source;
                }
            }

            return string.Empty;
        }

        #endregion
    }
}
