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

        public static T MinBy<T>(this IEnumerable<T> src, Func<T, int> getLength) {
            return src.OrderBy(getLength).First();
        }

        public static T MaxBy<T>(this IEnumerable<T> src, Func<T, int> getLength) {
            return src.OrderByDescending(getLength).First();
        }

        public static IEnumerable<string> Prepend(this IEnumerable<string> src, string prefix) {
            return src.Select(x => prefix + x);
        }

        public static IEnumerable<string> Append(this IEnumerable<string> src, string suffix) {
            return src.Select(x => x + suffix);
        }

        public static IEnumerable<IReadOnlyList<T>> Split<T>(this IEnumerable<T> src, Func<T, bool> predicate) {
            var enumerator = src.GetEnumerator();
            var curResult = new List<T>();
            while (enumerator.MoveNext()) {
                if (predicate(enumerator.Current)) {
                    yield return curResult;
                    curResult = new List<T>();
                } else {
                    curResult.Add(enumerator.Current);
                }
            }
            yield return curResult;
        }
    }
}