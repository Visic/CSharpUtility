using System.ComponentModel;

namespace WPFUtility
{
    public class NotifyingProperty<T> : PropertyWithChangedAction<T>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public NotifyingProperty(T initialValue = default(T)) :
            base(x => { }, initialValue)
        {
            _changedAction = x => OnPropertyChanged();
        }

        protected virtual void OnPropertyChanged() {
            var copy = PropertyChanged;
            if(copy != null) copy(this, new PropertyChangedEventArgs("Value"));
        }
    }
}
