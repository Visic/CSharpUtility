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

    public class Option<T> : Union<Some<T>, None> {
        public Option(T value) : base(new Some<T>(value)) { }
        public Option() : base(new None()) { }
        
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

        public static implicit operator Option<T>(Some<T> someT) {
            return new Option<T>(someT);
        }

        public static implicit operator Option<T>(None none) {
            return new Option<T>();
        }

        public static implicit operator Option<T>(T val) {
            return new Option<T>(val);
        }
    }
}
