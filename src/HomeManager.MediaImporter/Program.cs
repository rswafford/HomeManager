using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.MediaImporter
{
    class Program
    {
        private static string _movieEndpoint;
        private static string _tvEndpoint;
        private static string _username;
        private static string _password;
        private static string _basicCredentials;

        static void Main(string[] args)
        {
            LogMessage("Beginning Import Process...");
            string movieDirectoryList = ConfigurationManager.AppSettings["movie-directory"];
            string tvDirectoryList = ConfigurationManager.AppSettings["tv-directory"];

            _movieEndpoint = ConfigurationManager.AppSettings["movie-endpoint"];
            LogMessageFormat("Movies will post to {0}", _movieEndpoint);
            _tvEndpoint = ConfigurationManager.AppSettings["tv-endpoint"];
            LogMessageFormat("TV Shows will post to {0}", _tvEndpoint);

            _username = ConfigurationManager.AppSettings["username"];
            _password = ConfigurationManager.AppSettings["password"];
            _basicCredentials = EncodeTo64(string.Format("{0}:{1}", _username, _password));


            LogMessage("Import complete.");
            Console.Read();
        }
        
        private static string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = Encoding.ASCII.GetBytes(toEncode);
            string returnValue = Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;
        }

        private static void LogMessage(string message)
        {
            Console.WriteLine(message);

        }

        private static void LogMessageFormat(string message, params object[] formatParameters)
        {
            LogMessage(string.Format(message, formatParameters));
        }
    }
}
