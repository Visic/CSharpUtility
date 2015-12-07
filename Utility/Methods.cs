using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

        public static string HashFile(string filePath) {
            using(var hash = HashAlgorithm.Create()) {
                using(var stream = File.OpenRead(filePath)) {
                    return Encoding.ASCII.GetString(hash.ComputeHash(stream));
                }
            }
        }

        public static string Hash(string str) {
            using(var hash = HashAlgorithm.Create()) {
                var content = Encoding.ASCII.GetBytes(str);
                return Encoding.ASCII.GetString(hash.ComputeHash(content));
            }
        }
    }
}
