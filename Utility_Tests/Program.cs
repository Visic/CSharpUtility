using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Utility;

namespace Utility_Tests {
    class Program {
        static void Main(string[] args) {
            var ol = new OrderedList<int>();
            ol.Add(0);
            ol.Add(3);
            ol.Add(1);
            Console.WriteLine(ol[2]);
            ol.Remove(3);
            ol.Add(2);
            Console.WriteLine($"{ol[1]} == 1? {ol[1] == 1}");
            ol.Remove(1);
            ol.Add(1);
            Console.WriteLine($"{ol[1]} == 1? {ol[1] == 1}");


            //Console.ReadKey();
        }
    }
}
