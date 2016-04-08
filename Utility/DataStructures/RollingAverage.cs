using System.Collections.Generic;
using System.Linq;

namespace Utility {
    public class RollingAverage {
        Queue<double> _buffer;
        int _samples;

        public RollingAverage(int samples) {
            _samples = samples;
            _buffer = new Queue<double>(samples);
        }

        public double Value { get; private set; }

        public void Update(double newVal) {
            if (_buffer.Count == _samples) _buffer.Dequeue();

            _buffer.Enqueue(newVal);
            Value = _buffer.Average();
        }

        public static implicit operator double(RollingAverage avg) {
            return avg.Value;
        }

        public override string ToString() {
            return Value.ToString();
        }
    }
}
