using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.Core
{
    public static class DictionaryExtensions
    {
        public static object lockObjec = new object();
        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key, Func<TKey, TValue> fun)
        {
            TValue value;
            if (!source.TryGetValue(key, out value))
            {
                value = fun(key);
                lock (lockObjec)
                {
                    if (!source.ContainsKey(key))
                        source.Add(key, value);
                }
            }
            return value;
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }
        public static ISet<T> Reload<T>(this ISet<T> set, IEnumerable<T> list)
        {
            set.Clear();
            foreach (var item in list)
            {
                set.Add(item);
            }
            return set;
        }
    }
}
