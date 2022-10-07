using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockByBlock.helpers
{
    public static class CollectionHelpers
    {
        public static void AddRange(this IList collection, IEnumerable items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        public static void AddRange(this IList collection, ICollection<object> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        public static void AddRange(this IList collection, HashSet<object> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        public static object? RemoveAndReturn(this IList collection, int index)
        {
            var item = collection[index];
            collection.RemoveAt(index);
            return item;
        }

        public static void RemoveAll(this IList collection, IList list)
        {
            foreach(object o in list)
            {
                collection.Remove(o);
            }
        }

        public static void RemoveAll(this IList collection, HashSet<object> list)
        {
            foreach (object o in list)
            {
                collection.Remove(o);
            }
        }

        // HashSet
        public static void AddAll(this HashSet<object> collection, IEnumerable items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        public static void RemoveAll(this HashSet<object> collection, IEnumerable list)
        {
            foreach (object o in list)
            {
                collection.Remove(o);
            }
        }

        public static T[] SubArray<T>(this T[] array, int offset, int length)
        {
            T[] result = new T[length];
            Array.Copy(array, offset, result, 0, length);
            return result;
        }
    }
}
