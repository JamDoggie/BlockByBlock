using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockByBlock.java_extensions
{
    public static class ListExtensions
    {
        public static T RemoveAndReturn<T>(this IList<T> list, int i)
        {
            T obj = list[i];
            list.RemoveAt(i);
            return obj;
        }
        
        /// <summary>
        /// Helper method that removes a key from the given dictionary and returns its previously associated value.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static V RemoveAndReturn<K,V>(this IDictionary<K,V> dictionary, K key)
        {
            V obj = dictionary[key];
            dictionary.Remove(key);
            return obj;
        }

        public static object? RemoveAndReturn(this System.Collections.IDictionary dictionary, object key)
        {
            if (!dictionary.Contains(key))
                return null;

            object obj = dictionary[key];
            dictionary.Remove(key);
            return obj;
        }
    }
}
