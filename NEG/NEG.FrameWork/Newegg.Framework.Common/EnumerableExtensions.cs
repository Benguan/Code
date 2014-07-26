/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Colin Chen (colin.z.chen@newegg.com)
 * Create Date:  1/8/2013 1:54:04 PM
 * Usage:
 *
 * Revision History
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;

namespace Newegg.Framework
{
    /// <summary>
    /// Enumerable Extensions.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Check whether a collection is null or empty.
        /// </summary>
        /// <typeparam name="TSource">IEnumerable T Class.</typeparam>
        /// <param name="source">IEnumerable Object.</param>
        /// <returns>Return True or False.</returns>
        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source == null || !source.Any();
        }

        /// <summary>
        /// Do action for each item.
        /// </summary>
        /// <typeparam name="TSource">T Class.</typeparam>
        /// <param name="source">Current IEnumerable.</param>
        /// <param name="action">Action for each item.</param>
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("mapFunction");
            }

            if (!source.IsNullOrEmpty())
            {
                foreach (TSource item in source)
                {
                    action(item);
                }
            }
        }

        /// <summary>
        /// Convert a collection To another collection.
        /// </summary>
        /// <typeparam name="TSource">Original Class.</typeparam>
        /// <typeparam name="TResult">Result Class.</typeparam>
        /// <param name="source">IEnumerable with TClass.</param>
        /// <param name="func">Function for each item.</param>
        /// <returns>Return IEnumerable ITResult  Or Null.</returns>
        public static List<TResult> ToList<TSource, TResult>(this IEnumerable<TSource> source, Func<IEnumerable<TResult>> func)
        {
            List<TResult> resultList = new List<TResult>();

            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (!source.IsNullOrEmpty())
            {
                var tempResultList = func() ?? Enumerable.Empty<TResult>();
                tempResultList = tempResultList.Where(n => n != null);
                resultList = tempResultList.ToList();
            }

            return resultList;
        }

        /// <summary>
        /// Convert a collection To another collection.
        /// </summary>
        /// <typeparam name="TSource">Original Class.</typeparam>
        /// <typeparam name="TResult">Result Class.</typeparam>
        /// <param name="source">IEnumerable with TClass.</param>
        /// <param name="selector">Delegate selector.</param>
        /// <returns>Return IEnumerable ITResult  Or Null.</returns>
        public static List<TResult> ToList<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            return source.ToList<TSource, TResult>(() => source.Select(selector));
        }
    }
}
