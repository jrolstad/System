using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace Rolstad.Extensions
{
    /// <summary>
    /// Extensions for Enumerable
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Runs an action on each item in an enumeration
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Enumeration to run on</param>
        /// <param name="action">Action to run</param>
        public static IEnumerable<T> Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            if(source == null) throw new ArgumentNullException("source");
            if(action == null) throw new ArgumentNullException("action");

            foreach (var item in source)
            {
                action(item);
            }

            return source;
        }

        /// <summary>
        /// Segments a given enumerable into a group of enumerations of a certain size
        /// </summary>
        /// <typeparam name="T">Type in the enumeration</typeparam>
        /// <param name="enumerable">Enumeration containing the items to segment</param>
        /// <param name="segmentSize">Maximum size a segment can be</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Segment<T>(this IEnumerable<T> enumerable, int segmentSize)
        {
            IEnumerable<IEnumerable<T>> segmented = null;

            if (segmentSize <= 0)
            {
                throw new ArgumentOutOfRangeException("segmentSize", segmentSize, "segmentSize must be larger than zero");
            }

            if (enumerable != null)
            {
                var enumerableArray = enumerable.ToArray();
                segmented = Enumerable.Range(0, enumerableArray.Length)
                    .GroupBy(i => i / segmentSize, i => enumerableArray[i])
                    .ToArray();
            }
            return segmented;
        }

        /// <summary>
        /// Converts an enumerable to a dictionary.  If it can't, then gives a good reason why
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="elementSelector"></param>
        /// <returns></returns>
        public static Dictionary<TKey, TElement> ToDictionaryExplicit<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)       
        {
            if(source == null) throw new ArgumentNullException("source");
            if(keySelector.Equals(null)) throw new ArgumentNullException("keySelector");
            if(elementSelector.Equals(null)) throw new ArgumentNullException("keySelector");

            try
            {
                return source.ToDictionary(keySelector, elementSelector);
            }
            catch (ArgumentException exception)
            {
                var duplicates = source.GroupBy(keySelector).Where(g => g.Count() > 1).Select(g => g.Key).ToArray();
                var duplicateMessage = string.Join(",", duplicates);
                var message = "Unable to convert to dictionary since keys are not unique. Duplicate keys are: {0}".StringFormat(duplicateMessage);

                throw new ArgumentException(message,exception);
            }
        }

        /// <summary>
        /// Converts an enumerable to a dictionary.  If it can't, then gives a good reason why
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static Dictionary<TKey, TSource> ToDictionaryExplicit<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if(source == null) throw new ArgumentNullException("source");
            if(keySelector.Equals(null)) throw new ArgumentNullException("keySelector");

            try
            {
                return source.ToDictionary(keySelector);
            }
            catch (ArgumentException exception)
            {
                var duplicates = source.GroupBy(keySelector).Where(g => g.Count() > 1).Select(g => g.Key).ToArray();
                var duplicateMessage = string.Join(",", duplicates);
                var message = "Unable to convert to dictionary since keys are not unique. Duplicate keys are: {0}".StringFormat(duplicateMessage);

                throw new ArgumentException(message, exception);
            }
        }

        /// <summary>
        /// Tries to get a value from a dictionary.  If it can't, then tells what the keys are
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue Value<TKey,TValue>(this IDictionary<TKey,TValue> source,TKey key )
        {
            if(source == null) throw new ArgumentNullException("source");
            if(key.Equals(null)) throw new ArgumentNullException("key");

            try
            {
                return source[key];
            }
            catch (KeyNotFoundException exception)
            {
                var keysMessage = string.Join(",", source.Keys);
                var message = "Unable to find value for key '{0}'. Available keys are: {0}".StringFormat(keysMessage);

                throw new KeyNotFoundException(message, exception);
            }
        }
    }
}