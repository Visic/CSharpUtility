using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility {
    public class InfiniteDecimalApproximation {
        int _previousState;
        double _targetResult, _step;
        Func<double, double> _getResult;

        public InfiniteDecimalApproximation(Func<double, double> getResult, double targetResult, double initialStep) {
            _getResult = getResult;
            _targetResult = targetResult;
            _step = initialStep;
        }

        public double CurResult { get { return _getResult(Value); } }
        public double Value { get; private set; }

        public void Iterate() {
            var state = CurResult.CompareTo(_targetResult);
            if(state > 0) {
                Value -= _step;
            } else if (state < 0) {
                Value += _step;
            }
            if (state > 0 && _previousState < 0) _step /= 10;
            _previousState = state;
        }
    }
}
