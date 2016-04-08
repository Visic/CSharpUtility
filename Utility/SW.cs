using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Utility {
    public static class SW {
        static Dictionary<string, Stopwatch> _stopwatches = new Dictionary<string, Stopwatch>();

        public static void Restart(string name) {
            if(!_stopwatches.ContainsKey(name)) _stopwatches[name] = new Stopwatch();
            _stopwatches[name].Restart();
        }

        public static void RestartAndPrint(string name, string msg = "", long threshholdInMS = 0) {
            var ms = _stopwatches[name].Elapsed.TotalMilliseconds;
            if(ms >= threshholdInMS) Console.WriteLine("{0} -- Elapsed {1}ms", msg, ms);
            _stopwatches[name].Restart();
        }

        public static void StopAndPrint(string name, string msg = "", long threshholdInMS = 0) {
            _stopwatches[name].Stop();
            var ms = _stopwatches[name].Elapsed.TotalMilliseconds;
            if(ms >= threshholdInMS) Console.WriteLine("{0} -- Elapsed {1}ms", msg, ms);
        }

        public static double StopAndElapsed(string name) {
            return _stopwatches[name].Elapsed.TotalMilliseconds;
        }
    }
}
