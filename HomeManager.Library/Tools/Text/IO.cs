using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Library.Tools.Text
{
    public static class IO
    {
        public static string ReadTextFromFile(string path)
        {
            if (!File.Exists(path))
            {
                return string.Empty;
            }

            return File.ReadAllText(path);
        }

        public static void WriteTextToFile(string path, string value)
        {
            var textWriter = new StreamWriter(path, false, Encoding.UTF8);

            try
            {
                textWriter.Write(value);

            }
            finally
            {
                textWriter.Flush();
                textWriter.Close();
            }
        }
    }
}
