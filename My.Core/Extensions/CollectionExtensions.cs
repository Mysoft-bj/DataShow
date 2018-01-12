using System;
using System.Collections.Generic;

namespace My.Core
{
    /// <summary>
    /// Extension methods for Collections.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Checks whatever given collection object is null or has no item.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }
        public static string JoinAsString(this IEnumerable<string> source, string separator)
        {
            return string.Join(separator, source);
        }
        public static string JoinAsString(this IEnumerable<string> source)
        {
            return string.Join(",",source);
        }
      
    }
}