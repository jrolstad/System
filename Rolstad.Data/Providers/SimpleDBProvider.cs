using System;
using System.Collections.Generic;
using System.Linq;
using ScrappyDB;
using log4net;

namespace Rolstad.Data.Providers
{
    /// <summary>
    /// Provider to AWS SimpleDB using the ScrappyDB provider
    /// </summary>
    /// <typeparam name="T">Type to persist</typeparam>
    /// <typeparam name="I">Type of the identifier</typeparam>
    [Obsolete("Use Directus.SimpleDB instead")]
    public class SimpleDBProvider<T,I>:IDataProvider<T,I> where T : class, new()
    {
        private static ILog Logger = LogManager.GetLogger(typeof (SimpleDBProvider<T, I>));

        private readonly IDb _simpleDB;

        /// <summary>
        /// Constructor for DI
        /// </summary>
        /// <param name="simpleDB">Database instance</param>
        public SimpleDBProvider(IDb simpleDB)
        {
            _simpleDB = simpleDB;
        }

        public SimpleDBProvider():this(new Db())
        {
            
        }

        /// <summary>
        /// Obtains a single instance
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public T Get(I identifier)
        {
            if(Logger.IsDebugEnabled)Logger.DebugFormat("Get: Finding item {0}", identifier);

            var item = _simpleDB.Find<T>(identifier);

            if (Logger.IsDebugEnabled) Logger.DebugFormat("Get: Item {0} was {1}", item == null ? "not found": "found");

            return item;
        }

        /// <summary>
        /// Obtains all instances
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            if (Logger.IsDebugEnabled) Logger.Debug("GetAll: Requesting Items");

            var items = _simpleDB.Query<T>();

            if (Logger.IsDebugEnabled) Logger.DebugFormat("GetAll: {0} items found",items.Count);

            return items;
        }

        /// <summary>
        /// Performs a search on the item
        /// </summary>
        /// <param name="filterCriteria"></param>
        /// <returns></returns>
        public IEnumerable<T> Search(Func<T,bool> filterCriteria)
        {
            if (Logger.IsDebugEnabled) Logger.DebugFormat("Search: Requesting Items matching {0}",filterCriteria);

            var items = _simpleDB.Query<T>().Where(filterCriteria);

            if (Logger.IsDebugEnabled) Logger.DebugFormat("Search: {0} items found", items.Count());

            return items;
        }

        /// <summary>
        /// Saves a given item
        /// </summary>
        /// <param name="item"></param>
        public void Save(T item)
        {
            if (Logger.IsDebugEnabled) Logger.DebugFormat("Save: Saving item {0}",item);

            _simpleDB.SaveChanges(item);

            if (Logger.IsDebugEnabled) Logger.Debug("Save: Save Complete");

        }

        /// <summary>
        /// Deletes a given item
        /// </summary>
        /// <param name="identifier"></param>
        public void Delete(I identifier)
        {
            if (Logger.IsDebugEnabled) Logger.DebugFormat("Delete: Deleting item {0}", identifier);

            _simpleDB.Delete<T>(identifier);

            if (Logger.IsDebugEnabled) Logger.DebugFormat("Delete: Delete complete for item {0}", identifier);

        }
    }
}