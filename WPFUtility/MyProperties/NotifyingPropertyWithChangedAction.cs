using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUtility {
    public class NotifyingPropertyWithChangedAction<T> : NotifyingProperty<T> {
        public NotifyingPropertyWithChangedAction(Action<T> changedAction, T initialValue = default(T)) 
            : base(initialValue) 
        {
            var notifyAction = _changedAction;
            _changedAction = x => {
                changedAction(x);
                notifyAction(x);
            };
        }
    }
}
