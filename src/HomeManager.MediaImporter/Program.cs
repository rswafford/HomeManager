using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Library.Settings;
using HomeManager.Library.Tools.Enums;
using HomeManager.Library.Tools.FileUtil;
using HomeManager.Library.Tools.Importing;
using HomeManager.Model.RequestModels;
using log4net;
using log4net.Config;

namespace HomeManager.MediaImporter
{
    class Program
    {
        private static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static string _movieEndpoint;
        private static string _tvEndpoint;
        private static string _username;
        private static string _password;

        static void Main(string[] args)
        {
            var logFile = new FileInfo("log4net.config");
            XmlConfigurator.Configure(logFile);

            LogMessage("Beginning Import Process...");
            string movieDirectoryList = ConfigurationManager.AppSettings["movie-directory"];
            string[] movieDirectories = movieDirectoryList.Split(',');

            string tvDirectoryList = ConfigurationManager.AppSettings["tv-directory"];
            string[] tvDirectories = tvDirectoryList.Split(',');

            _movieEndpoint = ConfigurationManager.AppSettings["movie-endpoint"];
            LogMessageFormat("Movies will post to {0}", _movieEndpoint);
            _tvEndpoint = ConfigurationManager.AppSettings["tv-endpoint"];
            LogMessageFormat("TV Shows will post to {0}", _tvEndpoint);

            _username = ConfigurationManager.AppSettings["username"];
            _password = ConfigurationManager.AppSettings["password"];

            System.Net.ServicePointManager.ServerCertificateValidationCallback =
                ((sender, certificate, chain, sslPolicyErrors) => true);
            /*
             * foreach movie directory
             *  get files
             *  foreach file
             *      post movie
             */
            var movieFiles = GetAllFiles(movieDirectories);
            foreach(var movieFile in movieFiles)
            {
                MovieImportRequestModel model = BuildMovieImportModel(movieFile);
                PostMovie(model);
            }
             /*  
             * foreach tv directory
             *  get files
             *      foreach file
             *          post tv
             */
            var tvFiles = GetAllFiles(tvDirectories);
            foreach(var tvFile in tvFiles)
            {
                var episodeNumbers = TvNaming.ExtractEpisodeNumber(tvFile);
                foreach(var ep in episodeNumbers)
                {
                    TvShowImportRequestModel model = BuildTvImportModel(tvFile, ep);
                    PostTvEpisode(model);
                }
            }

            LogMessage("Import complete.");
            Console.Read();
        }

        private static TvShowImportRequestModel BuildTvImportModel(string filename, int episodeNumber)
        {
            TvShowImportRequestModel model = new TvShowImportRequestModel
                {
                    Episode = episodeNumber,
                    FileHash = FileHash.ComputeMovieHash(filename),
                    Format = DetectType.FindVideoSource(filename),
                    Filename = Path.GetFileName(filename),
                    FullPath = filename,
                    Season = TvNaming.ExtractSeasonNumber(filename),
                    SeriesName = TvNaming.ExtractSeriesName(filename)
                };

            return model;
        }

        private static MovieImportRequestModel BuildMovieImportModel(string filename)
        {
            MovieImportRequestModel model = new MovieImportRequestModel
                {
                    FileHash = FileHash.ComputeMovieHash(filename),
                    Filename = Path.GetFileName(filename),
                    Format = DetectType.FindVideoSource(filename),
                    FullPath = filename,
                    Title = MovieNaming.GetMovieName(filename, AddFolderType.NameByFile),
                    Year = MovieNaming.GetMovieYear(Path.GetFileName(filename))
                };

            return model;
        }

        private static IEnumerable<string> GetAllFiles(IEnumerable<string> directoryList)
        {
            var retVal = new List<string>();
            foreach(var dir in directoryList)
            {
                foreach (var extension in Get.VideoExtensions)
                {
                    retVal.AddRange(Directory.GetFiles(dir, "*." + extension, SearchOption.AllDirectories));
                }
            }
            return retVal;
        }

        private static void PostMovie(MovieImportRequestModel model)
        {
            var request = HttpRequestMessageHelper.ConstructRequest(HttpMethod.Post, _movieEndpoint, "application/json",
                                                                    _username, _password);
            request.Content = new ObjectContent<MovieImportRequestModel>(model, new JsonMediaTypeFormatter());

            var response = GetResponse(request);
            if(!response.IsSuccessStatusCode)
            {
                LogMessageFormat("Received status {0} for movie {1}", response.StatusCode, model.Title);
            }
            else
            {
                LogMessageFormat("Posted movie {0} successfully!", model.Title);
            }
        }

        private static void PostTvEpisode(TvShowImportRequestModel model)
        {
            var request = HttpRequestMessageHelper.ConstructRequest(HttpMethod.Post, _tvEndpoint, "application/json",
                                                                    _username, _password);

            request.Content = new ObjectContent<TvShowImportRequestModel>(model, new JsonMediaTypeFormatter());
            var response = GetResponse(request);
            if (!response.IsSuccessStatusCode)
            {
                LogMessageFormat("Received status {0} for TV Episode {1} Season {2} Episode {3}", response.StatusCode, model.SeriesName, model.Season, model.Episode);
            }
            else
            {
                LogMessageFormat("Posted TV Episode {0} Season {1} Episode {2} successfully!", model.SeriesName, model.Season, model.Episode);
            }
        }

        internal static HttpResponseMessage GetResponse(HttpRequestMessage request)
        {
            using (var client = HttpClientFactory.Create())
            {
                return client.SendAsync(request).Result;
            }
        }

        private static void LogMessage(string message)
        {
            Console.WriteLine(message);
            Log.Debug(message);
        }

        private static void LogMessageFormat(string message, params object[] formatParameters)
        {
            LogMessage(string.Format(message, formatParameters));
        }
    }
}
