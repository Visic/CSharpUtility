using System;

namespace Utility {
    public static class Cached {
        public static Cached<T> New<T>(Func<T> gen) {
            return new Cached<T>(gen);
        }
    }

    public class Cached<T> {
        Func<T> _gen;
        Option<T> _cachedValue;

        public Cached(Func<T> gen) {
            _gen = gen;
        }

        public T Value { get {
            if (_cachedValue.IsNone) _cachedValue = _gen();
            return _cachedValue.Value;
        } }

        public void Invalidate() {
            _cachedValue = new None();
        }

        public static implicit operator T(Cached<T> tgt) {
            return tgt.Value;
        }
    }
}