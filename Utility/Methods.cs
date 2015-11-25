using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility {
    public static class Methods {
        public static bool IsApproximately(int left, int right, int tolerance) {
            return Math.Abs(left - right) < tolerance;
        }

        public static bool IsApproximately(double left, double right, double tolerance) {
            return Math.Abs(left - right) < tolerance;
        }
    }
}
