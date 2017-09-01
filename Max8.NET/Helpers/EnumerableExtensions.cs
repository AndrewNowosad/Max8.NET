using System.Collections.Generic;
using System.Linq;

namespace Max8.NET.Helpers
{
    static class EnumerableExtensions
    {
        public static int IndexOf<T>(this IReadOnlyList<T> source, T value)
        {
            for (int i = 0; i < source.Count(); ++i)
                if (EqualityComparer<T>.Default.Equals(source[i], value)) return i;
            return -1;
        }
    }
}