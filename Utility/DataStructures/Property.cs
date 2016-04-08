using System;

namespace Utility {
    public class Property<T> {
        protected Func<T, T, bool> _changingAction;
        protected Action<T> _changedAction;

        public Property(T initialValue = default(T)) : this((o, n) => true, x => { }, initialValue) { }
        public Property(Action<T, T> changing, T initialValue = default(T)) : this(MakeChanging(changing), x => { }, initialValue) { }
        public Property(Func<T, T, bool> changing, T initialValue = default(T)) : this(changing, x => { }, initialValue) { }
        public Property(Action<T> changed, T initialValue = default(T)) : this((o, n) => true, changed, initialValue) { }
        public Property(Action<T, T> changing, Action<T> changed, T initialValue = default(T)) : this(MakeChanging(changing), changed, initialValue) { }

        public Property(Func<T, T, bool> changing, Action<T> changed, T initialValue = default(T)) {
            _changingAction = changing;
            _changedAction = changed;
            _value = initialValue;
        }

        protected T _value;
        public T Value {
            get { return _value; }
            set {
                if((value == null) ? _value != null : !value.Equals(_value)) {
                    if(!_changingAction(_value, value)) return;
                    _value = value;
                    _changedAction(value);
                }
            }
        }

        protected static Func<T, T, bool> MakeChanging(Action<T, T> changing) {
            return (o, n) => {
                changing(o, n);
                return true;
            };
        }
    }
}
