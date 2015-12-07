using System;
using System.ComponentModel;

namespace WPFUtility {
    public abstract class ViewModelBase : INotifyPropertyChanged, IViewModelBase {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void Dispose() { }

        protected void OnPropertyChanged(string propName) {
            var copy = PropertyChanged;
            if(copy != null) PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
