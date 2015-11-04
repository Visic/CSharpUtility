using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUtility
{
    public class PropertyWithChangedAction<T>
    {
        protected Action<T> _changedAction;

        public PropertyWithChangedAction(Action<T> changedAction, T initialValue = default(T)) {
            _changedAction = changedAction;
            _value = initialValue;
        }

        public T _value;
        public T Value {
            get { return _value; }
            set {
                if ((value == null) ? _value != null : !value.Equals(_value)) {
                    _value = value;
                    _changedAction(value);
                }
            }
        }
    }
}
