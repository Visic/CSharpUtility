using System;

namespace Utility {
    public struct Some<T> { 
        T _value;

        internal Some(T value) {
            _value = value;
        }

        public static implicit operator Some<T>(T value) {
            return new Some<T>(value);
        }

        public static implicit operator T(Some<T> target) {
            return target._value;
        }
    }
}
