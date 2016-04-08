using System;
using System.ComponentModel;
using Utility;

namespace WPFUtility {
    public class NotifyingProperty<T> : Property<T>, INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        public NotifyingProperty(T initialValue = default(T)) : this((o, n) => true, x => { }, initialValue) { }
        public NotifyingProperty(Action<T, T> changing, T initialValue = default(T)) : this(MakeChanging(changing), x => { }, initialValue) { }
        public NotifyingProperty(Func<T, T, bool> changing, T initialValue = default(T)) : this(changing, x => { }, initialValue) { }
        public NotifyingProperty(Action<T> changed, T initialValue = default(T)) : this((o, n) => true, changed, initialValue) { }
        public NotifyingProperty(Action<T, T> changing, Action<T> changed, T initialValue = default(T)) : this(MakeChanging(changing), changed, initialValue) { }

        public NotifyingProperty(Func<T, T, bool> changing, Action<T> changed, T initialValue = default(T)) : base(initialValue) {
            _changingAction = changing;
            _changedAction = n => { changed(n); OnPropertyChanged(); };
        }

        protected virtual void OnPropertyChanged() {
            var copy = PropertyChanged;
            if(copy != null) copy(this, new PropertyChangedEventArgs("Value"));
        }
    }
}
