using System.Collections.Generic;
using System.Globalization;

namespace HomeManager.Library.Tools.Clean
{
    public static class RemovePrefix
    {

        /// <summary>
        /// Moves a prefix from the start of a title to the end.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns>The fixes title</returns>
        public static string Go(string title)
        {
            string[] prefixes = { "A", "An", "The", "Le", "The", "La", "Les", "Un", "Une", "De", "Het", "Een" };

            title = MoveFromStartToEnd(title, prefixes, true);
            return title;
        }

        /// <summary>
        /// Moves a value from start to end of a string
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="prefixes">The prefixes.</param>
        /// <param name="withComma">if set to <c>true</c> [with comma].</param>
        /// <returns>Fixed string</returns>
        private static string MoveFromStartToEnd(string value, IEnumerable<string> prefixes, bool withComma)
        {
            foreach (var s in prefixes)
            {
                if (value.StartsWith(s + " ", true, CultureInfo.CurrentCulture))
                {
                    value = value.Substring(s.Length + 1, value.Length - (s.Length + 1));

                    if (withComma)
                    {
                        value += ", " + s;
                    }
                }
            }

            return value;
        }
    }
}
