using System;

namespace WPFUtility
{
    public class ConditionalPropertyWithChangedAction<T>
    {
        protected Action<T> _changed;
        protected Func<T, T, bool> _changing;

        public ConditionalPropertyWithChangedAction(Func<T, T, bool> changing, Action<T> changed, T initialValue = default(T)) {
            _changing = changing;
            _changed = changed;
            _value = initialValue;
        }

        public T _value;
        public T Value {
            get { return _value; }
            set {
                if ((value == null ? _value != null : !value.Equals(_value)) && _changing(_value, value)) {
                    _value = value;
                    _changed(value);
                }
            }
        }
    }
}
