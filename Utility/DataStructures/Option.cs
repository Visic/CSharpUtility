using System;

namespace Utility {
    public static class Option {
        public static Option<T> New<T>() {
            return new Option<T>();
        }

        public static Option<T> New<T>(T value) {
            return new Option<T>(value);
        }
    }

    public class Option<T> : Union<T, None> {
        public Option(T value) : base(value) { }
        public Option() : base(new None()) { }

        public T Value {
            get {
                return Match(x => x, x => { throw new Exception("Value is not defined on None"); });
            }
        }

        public bool IsSome {
            get {
                return Match<bool>(x => true, x => false);
            }
        }

        public bool IsNone { get { return !IsSome; } }

        public Option<RT> Apply<RT>(Func<T, RT> func) {
            return Match<Option<RT>>(
                x => func(x),
                x => x
            );
        }

        public Option<RT> Apply<RT>(Func<T, Option<RT>> func) {
            return Match<Option<RT>>(
                x => func(x),
                x => x
            );
        }

        public void Apply(Action<T> action) {
            Match(
                x => action(x),
                x => { }
            );
        }

        public static implicit operator Option<T>(T someT) {
            return new Option<T>(someT);
        }

        public static implicit operator Option<T>(None none) {
            return new Option<T>();
        }
    }
}
