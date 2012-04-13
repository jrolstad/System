﻿using System;
using System.Collections.Generic;

namespace Rolstad.Data.Providers
{
    /// <summary>
    /// Interface for data providers
    /// </summary>
    /// <typeparam name="T">Type of item being provisioned</typeparam>
    /// <typeparam name="I">Identifier for the item</typeparam>
    [Obsolete]
    public interface IDataProvider<T,I>
    {
        /// <summary>
        /// Obtains a single item
        /// </summary>
        /// <param name="identifier">Identifier to get the item for</param>
        /// <returns></returns>
        T Get(I identifier);

        /// <summary>
        /// Obtains all of the items
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Search for a particular item
        /// </summary>
        /// <param name="filterCriteria"></param>
        /// <returns></returns>
        IEnumerable<T> Search(Func<T, bool> filterCriteria); 

        /// <summary>
        /// Saves a particular item
        /// </summary>
        /// <param name="item"></param>
        void Save(T item);

        /// <summary>
        /// Deletes a given item
        /// </summary>
        /// <param name="identifier"></param>
        void Delete(I identifier);
    }
}