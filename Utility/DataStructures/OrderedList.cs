using System;
using System.Collections.Generic;
using System.Collections;

//TODO:: Try to remove the need for "insert" entirely (the inserting into a list is a linear run time, thats why this is so slow still)
namespace Utility {
    public class OrderedList<T> : IReadOnlyList<T> where T : IComparable {
        List<T> _list = new List<T>();
        List<int> _sortedIndicies = new List<int>();
        int _unsortedIndex, _highestAccessedIndex = -1;

        public int Count { get { return _list.Count; } }

        public T this[int index]
        {
            get
            {
                SortIfNecessary(index);
                return _list[index];
            }
        }

        public void Add(T item) {
            _list.Add(item);
            if(_sortedIndicies.Count > 0 && _list[_sortedIndicies[0] - 1].CompareTo(item) < 0) {
                _highestAccessedIndex = -1;
            }
        }

        public void Clear() {
            _list.Clear();
            _sortedIndicies.Clear();
            _unsortedIndex = 0;
            _highestAccessedIndex = -1;
        }

        public bool Contains(T item) {
            return _list.Contains(item);
        }

        public bool Remove(T item) {
            var index = _list.LastIndexOf(item);
            if (index < 0) return false;

            if (index <= _highestAccessedIndex) --_highestAccessedIndex;
            if (index <= _unsortedIndex) --_unsortedIndex;
            Methods.For(_sortedIndicies, (i, x) => {
                if (index <= x) _sortedIndicies[i] = x - 1;
            });
            _list.RemoveAt(index);
            return true;
        }

        public int FindInsertionIndex(IComparable item) {
            SortIfNecessary(FindIndex_Rec(item, 0, (_sortedIndicies.Count == 0 ? Count : _sortedIndicies[0]) - 1));
            return FindIndex_Rec(item, 0, (_sortedIndicies.Count == 0 ? Count : _sortedIndicies[0]) - 1);
        }

        public List<T> GetRange(int index, int count) {
            SortIfNecessary(index + count);
            return _list.GetRange(index, count);
        }

        public void RemoveRange(int index, int count) {
            var offsetIndex = index + count;
            SortIfNecessary(offsetIndex);
            _list.RemoveRange(index, count);

            if (offsetIndex <= _highestAccessedIndex) _highestAccessedIndex -= offsetIndex;
            if (offsetIndex <= _unsortedIndex) _unsortedIndex -= offsetIndex;
            Methods.For(_sortedIndicies, (i, x) => {
                if (offsetIndex <= x) _sortedIndicies[i] = x - offsetIndex;
            });
        }

        //Sort _list up to the specified index
        private void SortIfNecessary(int index) {
            if(_highestAccessedIndex >= index) return;

            //Sort the unsorted part
            if(_unsortedIndex < Count) {
                _list.Sort(_unsortedIndex, Count - _unsortedIndex, null);
                if(_unsortedIndex > 0) _sortedIndicies.Add(_unsortedIndex); //otherwise this is the initial sort
                _unsortedIndex = Count;
            }

            if (_sortedIndicies.Count == 0) {
                if (index > _highestAccessedIndex) _highestAccessedIndex = index;
                return; //nothing to merge
            }

            //Merge blocks until we reach [index]
            for(int i = 0; i < _sortedIndicies.Count; ++i) {
                var mergeCount = (i == _sortedIndicies.Count - 1 ? Count : _sortedIndicies[i + 1]) - _sortedIndicies[i];
                if(mergeCount - 1 > index) mergeCount = index + 1; //limit amount to [index]
                var offsetMergeTo = mergeCount + _sortedIndicies[i];

                var y = 0; //y == index into sorted part of list
                for(int z = _sortedIndicies[i]; z < offsetMergeTo;) { //for all elements in the sorted block, less than mergeUpTo
                    y = FindIndex_Rec(_list[z], y, _sortedIndicies[0] - 1) + y; //find where it belongs as quickly as possible
                    var chunk = z + 1 < offsetMergeTo ? FindIndex_Rec(_list[y], z + 1, offsetMergeTo - 1) + 1 : 1;
                    _list.InsertRange(y, _list.GetRange(z, chunk));
                    _list.RemoveRange(z += chunk, chunk);
                    _sortedIndicies[i] += chunk;
                    y += chunk + 1;

                    if (y >= z) z = _sortedIndicies[i] = offsetMergeTo; //all elements in this sorted section have been handled
                }

                if(_sortedIndicies[i] == Count || (i < _sortedIndicies.Count - 1 && _sortedIndicies[i] == _sortedIndicies[i + 1])) {
                    _sortedIndicies.RemoveAt(i);
                }
            }

            _highestAccessedIndex = index;
        }

        //returns an index (relative to [startIndex]) where [ele] belongs
        private int FindIndex_Rec(IComparable ele, int startIndex, int endIndex) {
            if(_list[startIndex].CompareTo(ele) > 0) return 0;
            if(_list[endIndex].CompareTo(ele) <= 0) return endIndex + 1 - startIndex;

            var half = (endIndex - startIndex) / 2;
            if(half == 0) half = 1;
            return FindIndex_Rec(ele, startIndex, endIndex - half) + FindIndex_Rec(ele, endIndex - half + 1, endIndex);
        }

        #region IEnumerable
        public IEnumerator<T> GetEnumerator() {
            return OrderedEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return OrderedEnumerable().GetEnumerator();
        }

        private IEnumerable<T> OrderedEnumerable() {
            for(int i = 0; i < Count; ++i) {
                yield return this[i];
            }
        }
        #endregion
    }
}