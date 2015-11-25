using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUtility {
    public class SingleSelectionCollection<T> : IEnumerable<T>, IDisposable, INotifyCollectionChanged
        where T : ISelectableViewModel {
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        ObservableCollection<T> _selectableViewModels;

        public SingleSelectionCollection(params T[] selectableViewModels) {
            _selectableViewModels = new ObservableCollection<T>(selectableViewModels ?? new T[0]);
            _selectableViewModels.CollectionChanged += (s, e) => CollectionChanged?.Invoke(this, e);

            foreach(var ele in selectableViewModels) {
                ele.IsSelected.PropertyChanged += OnSelectionChanged;
            }
        }

        public void Add(T selectableObject) {
            if(!_selectableViewModels.Contains(selectableObject)) {
                selectableObject.IsSelected.PropertyChanged += OnSelectionChanged;
                _selectableViewModels.Add(selectableObject);
            }
        }

        public void Insert(int index, T selectableObject) {
            if(!_selectableViewModels.Contains(selectableObject)) {
                selectableObject.IsSelected.PropertyChanged += OnSelectionChanged;
                _selectableViewModels.Insert(index, selectableObject);
            }
        }

        public void Remove(T selectableObject) {
            if(_selectableViewModels.Contains(selectableObject)) {
                selectableObject.IsSelected.PropertyChanged -= OnSelectionChanged;
                _selectableViewModels.Remove(selectableObject);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator() {
            return _selectableViewModels.GetEnumerator();
        }

        bool _updatingSelections;
        private void OnSelectionChanged(object sender, PropertyChangedEventArgs e) {
            var sourceProp = (NotifyingProperty<bool>)sender;
            if(sourceProp.Value && !_updatingSelections) {
                _updatingSelections = true;
                foreach(var ele in _selectableViewModels) {
                    ele.IsSelected.Value = false;
                }
                sourceProp.Value = true;
                _updatingSelections = false;
            }
        }

        public void Dispose() {
            foreach(var ele in _selectableViewModels) {
                ele.IsSelected.PropertyChanged -= OnSelectionChanged;
            }
        }
    }
}
