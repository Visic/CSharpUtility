namespace Utility {
    public struct Average {
        public Average(double val) : this() {
            Update(val);
        }

        public ulong Count { get; private set; }
        public double Value { get; private set; }

        public void Update(double newVal) {
            Value += (newVal - Value) / ++Count;
        }

        public static implicit operator double(Average avg) {
            return avg.Value;
        }

        public static implicit operator Average(double val) {
            return new Average(val);
        }

        public override string ToString() {
            return Value.ToString();
        }
    }
}