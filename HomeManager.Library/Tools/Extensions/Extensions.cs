using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HomeManager.Library.Tools.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Remove character returns from string
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The remove character return.
        /// </returns>
        public static string RemoveCharacterReturn(this string value)
        {
            value = Regex.Replace(value, "\n", string.Empty);
            value = Regex.Replace(value, "\r", string.Empty);

            return value;
        }

        /// <summary>
        /// Removes any instances of 2 or more spaces in a row.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The remove extra white space.
        /// </returns>
        public static string RemoveExtraWhiteSpace(this string value)
        {
            return Regex.Replace(value, @"\s{2,}", " ");
        }

        /// <summary>
        /// Replaces the specified value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <param name="replaceWith">
        /// The replace with.
        /// </param>
        /// <returns>
        /// The replace.
        /// </returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules",
            "SA1616:ElementReturnValueDocumentationMustHaveText", Justification = "Reviewed. Suppression is OK here.")]
        public static string Replace(this string value, string[] values, string replaceWith)
        {
            return values.Aggregate(value, (current, s) => current.Replace(s, replaceWith));
        }

        /// <summary>
        /// The get number from a string
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The first found number
        /// </returns>
        public static int GetNumber(this string value, int? max = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                return -1;
            }

            if (max == null)
            {
                max = 2;
            }

            string v = Regex.Match(value, "\\d{1," + max + "}").Groups[0].Value;
            return Convert.ToInt16(v);
        }
    }
}
