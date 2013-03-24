using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Library.Settings.ConstSettings
{
    public static class FileSystemPaths
    {
        public static string PathSettings
        {
            get
            {
                if (!ConfigurationManager.AppSettings.AllKeys.Contains("path-settings") ||
                    string.IsNullOrEmpty(ConfigurationManager.AppSettings["path-settings"]))
                {
                    return string.Format(
                        CultureInfo.CurrentCulture,
                        "{0}{1}Settings{1}",
                        Directory.GetCurrentDirectory(),
                        Path.DirectorySeparatorChar);
                }

                return ConfigurationManager.AppSettings["path-settings"];
            }
        }
    }
}
