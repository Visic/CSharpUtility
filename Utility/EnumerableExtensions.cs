using System;
using System.Collections.Generic;
using System.Linq;

namespace Utility
{
    public static class EnumerableExtensions {
        public static IEnumerable<IGrouping<TKey, T>> GroupBy<T, TKey>(this IEnumerable<T> src, Func<T, long, TKey> keySelector) {
            var i = -1L;
            return src.GroupBy(x => keySelector(x, ++i));
        }

        public static string ToDelimitedString<T>(this IEnumerable<T> src, string delimiter) {
            return string.Join(delimiter, src);
        }

        public static IEnumerable<string> PrependEach(this IEnumerable<string> src, string str) {
            return src.Select(x => str + x);
        }

        public static IEnumerable<string> AppendEach(this IEnumerable<string> src, string str) {
            return src.Select(x => x + str);
        }
    }
}