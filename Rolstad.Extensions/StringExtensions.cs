using System;
using System.ComponentModel;

namespace Rolstad.Extensions
{
    /// <summary>
    /// Extension Methods for strings
    /// </summary>
    [Obsolete("Use Directus.Extensions instead")]
    public static class StringExtensions
    {
        /// <summary>
        /// If this is empty or not
        /// </summary>
        /// <param name="stringToEvaluate">String to determine if it is empty or not</param>
        /// <returns></returns>
        public static bool IsEmpty(this string stringToEvaluate)
        {
            return string.IsNullOrWhiteSpace(stringToEvaluate);
        }

        /// <summary>
        /// Determines if a string can be converted to a given type
        /// </summary>
        /// <typeparam name="T">Type to determine if the string is convertible to</typeparam>
        /// <param name="input">String to determine</param>
        /// <returns></returns>
        public static bool Is<T>(this string input)
        {
            try
            {
                TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(input);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Converts a string type to the defined type.  If <see langword="null"/> or empty, returns that type's default
        /// </summary>
        /// <typeparam name="T">Type to convert to</typeparam>
        /// <param name="value">Value to convert</param>
        /// <returns></returns>
        public static T To<T>(this string value)
        {
            var result = default(T);

            if (!value.IsEmpty())
            {
                // Get the underlying type for Nullable types
                // Since when trying to run Convert.ChangeType(value,Nullable<>) that will blow...
                // so instead, we get the underlying type
                if (typeof(T).IsGenericType
                    && typeof(T).GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    var t = typeof(T).GetGenericArguments()[0];
                    result = (T)Convert.ChangeType(value, t);
                }
                // Not a Nullable, get the type
                else
                {
                    result = (T)Convert.ChangeType(value, typeof(T));
                }
            }

            return result;
        }

        /// <summary>
        /// Formats a given string
        /// </summary>
        /// <param name="stringToFormat">String to format</param>
        /// <param name="param">Input values</param>
        /// <returns></returns>
        public static string StringFormat(this string stringToFormat, params object[] param)
        {
            return string.Format(stringToFormat, param);
        }

        /// <summary>
        /// Trims the white spaces from the front and the back of a string
        /// If the string is null, returns the null value
        /// </summary>
        /// <param name="value">The string to trim</param>
        /// <returns>The return value</returns>
        public static string SafeTrim(this string value)
        {
            return value == null ? null : value.Trim();
        }

        /// <summary>
        /// Null safe implementation of Contains()
        /// </summary>
        /// <param name="stringToEvaluate">String to evaluate</param>
        /// <param name="contains">String to search for</param>
        /// <returns></returns>
        public static bool SafeContains(this string stringToEvaluate, string contains)
        {
            return stringToEvaluate != null && stringToEvaluate.Contains(contains);
        }

        /// <summary>
        /// Performs a <see langword="null"/> safe to lower
        /// </summary>
        /// <param name="value">Value to trim</param>
        /// <returns>Trimmed value</returns>
        public static string SafeToLower(this string value)
        {
            // default to the value
            var result = value;

            // Trim if not null or empty
            if (string.IsNullOrEmpty(value) == false)
            {
                result = value.Trim().ToLower();
            }

            // Return the value
            return result;

        }
    }
}