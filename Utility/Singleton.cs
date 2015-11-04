using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility {
    public class Singleton<T>{
        static T _instance;

        public Singleton(Func<T> generator) {
            _instance = generator();
        }

        public T Instance() {
            return _instance;
        }
    }
}
