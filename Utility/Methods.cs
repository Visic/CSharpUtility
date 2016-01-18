using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

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

        public static IEnumerable<T> CreateSequence<T>(Func<Option<T>, Option<T>> elementGenerator) {
            var lastValue = new Option<T>();
            while(true) {
                lastValue = elementGenerator(lastValue);
                if(lastValue.IsNone) yield break;
                yield return lastValue.Value;
            }
        }

        public static Dictionary<char, ulong> CountCharOccurrences(string txt) {
            return CountCharOccurrences(txt, new Dictionary<char, ulong>());
        }

        public static T CountCharOccurrences<T>(string txt, T result) where T : IDictionary<char, ulong> {
            foreach(var ele in txt) {
                result.AddOrUpdate(ele, 1UL, v => v + 1);
            }
            return result;
        }
    }
}
