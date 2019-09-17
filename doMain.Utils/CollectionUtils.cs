using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace doMain.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class CollectionUtils
    {




        /// <summary>
        /// Moves the specified old index.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="oldIndex">The old index.</param>
        /// <param name="newIndex">The new index.</param>
        public static void Move<TSource>(this IList<TSource> list, int oldIndex, int newIndex)
        {
            var item = list[oldIndex];
            list.RemoveAt(oldIndex);
            list.Insert(newIndex, item);
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="destination">The destination.</param>
        /// <param name="source">The source.</param>
        public static void AddRange<T>(this ICollection<T> destination,
                               List<T> source)
        {
            List<T> list = destination as List<T>;

            if (list != null)
            {
                list.AddRange(source);
            }
            else
            {
                foreach (T item in source)
                {
                    destination.Add(item);
                }
            }
        }
    }
}
