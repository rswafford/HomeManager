using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Library.Settings.ConstSettings;
using HomeManager.Library.Settings.UserSettings;
using HomeManager.Library.Tools.Text;
using Newtonsoft.Json;
using log4net;

namespace HomeManager.Library.Settings
{
    public static class Get
    {
        private static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// The keywords settings
        /// </summary>
        private static Keywords _keywords;

        private static IEnumerable<string> _videoExtensions;

        /// <summary>
        /// Gets or sets keywords that will be used for filtering.
        /// </summary>
        /// <value>The keywords.</value>
        public static Keywords Keywords
        {
            get { return _keywords ?? (_keywords = new Keywords()); }
            set { _keywords = value; }
        }

        public static IEnumerable<string> VideoExtensions
        {
            get
            {
                return _videoExtensions ?? new List<string>
                {
                    "avi",
                    "divx",
                    "mkv",
                    "wmv",
                    "m2ts",
                    "ts",
                    "rm",
                    "qt",
                    "iso",
                    "vob",
                    "mpg",
                    "mov",
                    "mp4",
                    "m1v",
                    "m2v",
                    "m4v",
                    "m2p",
                    "tp",
                    "trp",
                    "m2t",
                    "mts",
                    "asf",
                    "rmp4",
                    "img",
                    "ifo",
                    "bdmv"
                };
            }
            set { _videoExtensions = value; }
        }

        /// <summary>
        /// Initializes static members of the <see cref="Get"/> class.
        /// </summary>
        static Get()
        {
            LoadAll();
        }

        public static void LoadAll()
        {
            LoadKeywordsSettings();
            _videoExtensions = new List<string>
                {
                    "avi",
                    "divx",
                    "mkv",
                    "wmv",
                    "m2ts",
                    "ts",
                    "rm",
                    "qt",
                    "iso",
                    "vob",
                    "mpg",
                    "mov",
                    "mp4",
                    "m1v",
                    "m2v",
                    "m4v",
                    "m2p",
                    "tp",
                    "trp",
                    "m2t",
                    "mts",
                    "asf",
                    "rmp4",
                    "img",
                    "ifo",
                    "bdmv"
                };
        }
        
        /// <summary>
        /// Save all settings
        /// </summary>
        public static void SaveAll()
        {
            SaveSettings(
                new dynamic[]
                    {
                        //Countries, Genres, Image, InOutCollection, 
                        Keywords 
                        //Localization, LogSettings, LookAndFeel, 
                        //Media, MediaInfo, Scraper, Ui, Web
                    });
        }
        
        /// <summary>
        /// The load keywords settings.
        /// </summary>
        private static void LoadKeywordsSettings()
        {
            try
            {
                string path = FileSystemPaths.PathSettings + "Keywords.txt";

                if (!File.Exists(path))
                {
                    return;
                }

                string json = IO.ReadTextFromFile(path);
                _keywords = JsonConvert.DeserializeObject(json, typeof(Keywords)) as Keywords;
            }
            catch (Exception exception)
            {
                Log.Error(exception);
            }
        }
        
        /// <summary>
        /// The save settings.
        /// </summary>
        /// <param name="objs">The object to save</param>
        private static void SaveSettings(IEnumerable<dynamic> objs)
        {
            try
            {
                foreach (dynamic obj in objs)
                {
                    dynamic path = FileSystemPaths.PathSettings +
                                   obj.GetType().ToString().Replace("HomeManager.Library.Settings.UserSettings.", string.Empty) + ".txt";
                    IO.WriteTextToFile(path, JsonConvert.SerializeObject(obj));
                }
            }
            catch (Exception exception)
            {
                Log.Error(exception);
            }
        }
    }
}
