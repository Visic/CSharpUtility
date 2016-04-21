namespace Utility {
    public class Reference<T> {
        public Reference(T val) { Value = val; }
        public T Value { get; }

        public static implicit operator T(Reference<T> reference) {
            return reference.Value;
        }

        public static implicit operator Reference<T>(T value) {
            return new Reference<T>(value);
        }
    }
}
