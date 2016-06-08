using System;
using System.Collections.Generic;

namespace Utility {
    public static class MiscExtensions {
        public static void AddOrUpdate<K, V>(this IDictionary<K, V> src, K key, V addValue, Func<V, V> updater) {
            if(!src.ContainsKey(key)) src[key] = addValue;
            else src[key] = updater(src[key]);
        }

        public static V GetOrAdd<K, V>(this IDictionary<K, V> src, K key, Func<V> lazyValue) {
            V result;
            if(!src.TryGetValue(key, out result)) src[key] = result = lazyValue();
            return result;
        }

        public static Option<V> TryGetValue<K, V>(this IDictionary<K, V> src, K key) {
            V result;
            if (!src.TryGetValue(key, out result)) return new Option<V>();
            return result;
        }

        public static T[] Reverse<T>(this T[] src) {
            var result = (T[])src.Clone();
            for(int i = 0; i < result.Length / 2; ++i) {
                Methods.Swap(ref result[result.Length - i - 1], ref result[i]);
            }
            return result;
        }

        public static List<T> Add_Ex<T>(this List<T> src, params T[] vals) {
            src.AddRange(vals);
            return src;
        }

        public static List<T> Insert_Ex<T>(this List<T> src, int index, params T[] vals) {
            src.InsertRange(index, vals);
            return src;
        }

        public static string Reverse(this string src) {
            return new string(src.ToCharArray().Reverse());
        }

        public static bool IsEven(this int src) {
            return (src & 1) == 0;
        }

        public static bool IsOdd(this int src) {
            return (src & 1) == 1;
        }

        public static string RemoveCount(this string str, int count, bool fromEnd) {
            if((str?.Length ?? 0) < count) return "";
            return str.Remove(fromEnd ? str.Length - count : 0, count);
        }
    }
}
