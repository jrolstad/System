using System;
using System.Globalization;

namespace Rolstad.Extensions
{
    /// <summary>
    /// Extension Methods for nullables
    /// </summary>
    public static class NullableExtensions
    {
        /// <summary>
        /// Null safe conversion to string for nullable
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <param name="formatString">Format to convert it to</param>
        /// <returns></returns>
        public static string ToString<T>(this T? value, string formatString) where T : struct,IFormattable
        {
            var formatProvider = CultureInfo.CurrentCulture.GetFormat(typeof(T)) as IFormatProvider;

            return value.ToString(formatString, formatProvider);
        }

        /// <summary>
        /// Null safe conversion to string for nullable
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <param name="formatString">Format to convert it to</param>
        /// <param name="formatProvider">Formatting provider</param>
        /// <returns></returns>
        public static string ToString<T>(this T? value, string formatString, IFormatProvider formatProvider) where T : struct,IFormattable
        {
            string result = null;
            if (value.HasValue)
            {
                result = value.Value.ToString(formatString, formatProvider);
            }

            return result;
        }
    }
}