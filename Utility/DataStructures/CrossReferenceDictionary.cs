using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Utility {
    public class CrossReferenceCollection<T1, T2> : IEnumerable<Tuple<T1, T2>>, IEnumerable {
        IEnumerable<Tuple<T1, T2>> _pairs;
        Dictionary<int, T1> _t1Lookup = new Dictionary<int, T1>(); //t2 hash -> t1 val
        Dictionary<int, T2> _t2Lookup = new Dictionary<int, T2>(); //t1 hash -> t2 val

        public CrossReferenceCollection() {
            _pairs = _t1Lookup.Select(x => Tuple.Create(x.Value, _t2Lookup[x.Value.GetHashCode()]));
        }
        
        public CrossReferenceCollection(IEnumerable<Tuple<T1, T2>> items) : this() {
            foreach(var ele in items) {
                Add(ele.Item1, ele.Item2);
            }
        }

        public virtual void Add(T1 t1, T2 t2) {
            _t2Lookup[t1.GetHashCode()] = t2;
            _t1Lookup[t2.GetHashCode()] = t1;
        }

        public virtual void Add(Tuple<T1, T2> tuple) {
            _t2Lookup[tuple.Item1.GetHashCode()] = tuple.Item2;
            _t1Lookup[tuple.Item2.GetHashCode()] = tuple.Item1;
        }

        public virtual bool RemoveByT1(T1 t1) {
            T2 t2;
            if (!_t2Lookup.TryGetValue(t1.GetHashCode(), out t2)) return false;
            _t2Lookup.Remove(t1.GetHashCode());
            _t1Lookup.Remove(t2.GetHashCode());
            return true;
        }

        public virtual bool RemoveByT2(T2 t2) {
            T1 t1;
            if (!_t1Lookup.TryGetValue(t2.GetHashCode(), out t1)) return false;
            _t1Lookup.Remove(t2.GetHashCode());
            _t2Lookup.Remove(t1.GetHashCode());
            return true;
        }

        public bool TryGetT2(T1 t1, out T2 t2) {
            return _t2Lookup.TryGetValue(t1.GetHashCode(), out t2);
        }

        public bool TryGetT1(T2 t2, out T1 t1) {
            return _t1Lookup.TryGetValue(t2.GetHashCode(), out t1);
        }

        public T2 GetT2(T1 t1) {
            return _t2Lookup[t1.GetHashCode()];
        }

        public T1 GetT1(T2 t2) {
            return _t1Lookup[t2.GetHashCode()];
        }

        public IEnumerator<Tuple<T1, T2>> GetEnumerator() {
            return _pairs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return _pairs.GetEnumerator();
        }
    }
}
