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

        public static Dictionary<TKey, TElement> ToDictionaryExplicit<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)       
        {
            try
            {
                return source.ToDictionary(keySelector, elementSelector);
            }
            catch (ArgumentException exception)
            {
                var duplicates = source.GroupBy(keySelector).Where(g => g.Count() > 1).Select(g => g.Key).ToArray();
                var duplicateMessage = string.Join(duplicates, ",");
                var message = "Unable to convert to dictionary since keys are not unique. Duplicate keys are: {0}".StringFormat();
                throw new ArgumentException(message,exception);
            }
        }
    }
}