using System;
using System.Collections.Generic;

namespace Rolstad.Extensions
{
    /// <summary>
    /// Extension methods for ICollection
    /// </summary>
    [Obsolete("Use Directus.Extensions instead")]
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds a range of items to the collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="itemsToAdd"></param>
        public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> itemsToAdd)
        {
            itemsToAdd.Each(source.Add);
        }
    }
}