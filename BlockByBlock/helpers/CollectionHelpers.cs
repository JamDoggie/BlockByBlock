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

        public static object? RemoveAndReturn(this IList collection, int index)
        {
            var item = collection[index];
            collection.RemoveAt(index);
            return item;
        }
    }
}
