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
    }
}
