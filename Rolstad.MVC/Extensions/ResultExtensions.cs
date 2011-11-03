using System;
using System.Web.Mvc;
using Rolstad.Extensions;

namespace Rolstad.MVC.Extensions
{
    public static class ResultExtensions
    {
        /// <summary>
        /// Obtains the model from a view result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static T Model<T>(this ViewResultBase result) where T : class
         {
             if(result == null) throw new ArgumentNullException("ViewResult can not be null");
             if(result.Model == null) throw new ArgumentNullException("ViewResult.Model can not be null");
             var data = result.Model as T;

             if(data == null) throw new ApplicationException("Unable to cast from type {0} to {1}".StringFormat(result.Model.GetType(),typeof(T)));

             return data;
         }

        /// <summary>
        /// OBtains the model from a JSON result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
         public static T Model<T>(this JsonResult result) where T : class
         {
             if (result == null) throw new ArgumentNullException("JsonResult can not be null");
             if (result.Data == null) throw new ArgumentNullException("JsonResult.Data can not be null");
             var data = result.Data as T;

             if (data == null) throw new ApplicationException("Unable to cast from type {0} to {1}".StringFormat(result.Data.GetType(), typeof(T)));

             return data;
         }
    }
}