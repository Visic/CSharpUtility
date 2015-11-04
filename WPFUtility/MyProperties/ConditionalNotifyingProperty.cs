using System;
using System.ComponentModel;

namespace WPFUtility
{
    public class ConditionalNotifyingProperty<T> : ConditionalPropertyWithChangedAction<T>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ConditionalNotifyingProperty(Func<T, T, bool> condition, T initialValue = default(T)) :
            base(condition, x => { }, initialValue)
        {
            _changed = x => OnPropertyChanged();
        }

        protected virtual void OnPropertyChanged() {
            var copy = PropertyChanged;
            if(copy != null) copy(this, new PropertyChangedEventArgs("Value"));
        }
    }
}
