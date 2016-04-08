using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

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

        public static void Order<T>(ref T v1, ref T v2) where T : IComparable {
            if (v1.CompareTo(v2) <= 0) return;
            Swap(ref v1, ref v2);
        }

        public static void Swap<T>(ref T v1, ref T v2) {
            var swap = v1;
            v1 = v2;
            v2 = swap;
        }

        public static void For(int i, Action<int> work) {
            for(int z = 0; z < i; ++z) {
                work(z);
            }
        }

        public static void For(int i, Action work) {
            for(int z = 0; z < i; ++z) {
                work();
            }
        }

        public static void For<T>(IEnumerable<T> enumerable, Action<T> work) {
            For(enumerable, (i, ele) => work(ele));
        }

        public static void For<T>(IEnumerable<T> enumerable, Action<int, T> work) {
            var arr = enumerable.ToArray();
            for(int i = 0; i < arr.Length; ++i) {
                work(i, arr[i]);
            }
        }

        /// <summary>   Performs the [updater] action until [condition] is true or [timeout] has elapsed </summary>
        /// <returns>   false if the timeout occurred, true otherwise </returns>
        public static bool DoUntilConditionIsTrueOrTimesOut(Func<bool> condition, Action updater, TimeSpan timeout, int pollInterval_ms = 100) {
            var timedout = false;
            var start = DateTime.UtcNow;
            do {
                updater();
                timedout = TimeSpan.Compare(DateTime.UtcNow.Subtract(start), timeout) > -1;
                Thread.Sleep(pollInterval_ms);
            } while (!condition() && !timedout);

            return !timedout;
        }
    }
}
