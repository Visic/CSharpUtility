using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utility;

namespace Utility_Tests {
    public class Base {
        public string DoSomethingCommon() {
            Console.WriteLine("Did something common!");
            return "Something else common!";
        }
    }

    public class Type1 : Base {
    }

    public class Type2 : Base {
    }

    public class Unique {
        public string UID = Guid.NewGuid().ToString();
    }

    class Program {
        static void Main(string[] args) {
            Random rnd = new Random();
            var limit = 1000000;
            var rndLimit = limit;// 100;

            Console.WriteLine("Add all first then verify---");
            for(int z = 10; z <= limit; z *= 10) {
                var sortedList = new List<int>();
                var oList = new OrderedList<int>();
                SW.Restart("");
                for(int i = 0; i < z; ++i) {
                    var n = rnd.Next(rndLimit);
                    oList.Add(n);
                    sortedList.Add(n);
                }
                sortedList.Sort();
                for(int i = 0; i < oList.Count; ++i) {
                    if(oList[i] != sortedList[i]) {
                        Console.WriteLine("Failed");
                        return;
                    }
                }
                SW.StopAndPrint("", z.ToString());
            }

            Console.WriteLine("Add then access---");
            for(int z = 10; z <= limit; z *= 10) {
                //var sortedList = new List<int>();
                var oList = new OrderedList<int>();
                SW.Restart("");
                for(int i = 0; i < z; ++i) {
                    var n = rnd.Next(rndLimit);
                    oList.Add(n);
                    var temp = oList[i];
                    //sortedList.Add(n);
                    //sortedList.Sort();
                    //if(oList[i] != sortedList[i]) {
                    //    Console.WriteLine("Failed");
                    //    return;
                    //}
                }
                SW.StopAndPrint("", z.ToString());
            }

            Console.WriteLine("Access after every 100 adds---");
            for(int z = 10; z <= limit; z *= 10) {
                //var sortedList = new List<int>();
                var oList = new OrderedList<int>();
                SW.Restart("");
                for(int i = 0; i < z; ++i) {
                    var n = rnd.Next(rndLimit);
                    oList.Add(n);
                    //sortedList.Add(n);
                    //sortedList.Sort();
                    if(i % 100 == 0) {
                        var temp = oList[i];
                        //if(oList[i] != sortedList[i]) {
                        //    Console.WriteLine("Failed");
                        //    return;
                        //}
                    }
                }
                SW.StopAndPrint("", z.ToString());
            }

            Console.WriteLine("Access after every 1000 adds---");
            for(int z = 10; z <= limit; z *= 10) {
                //var sortedList = new List<int>();
                var oList = new OrderedList<int>();
                SW.Restart("");
                for(int i = 0; i < z; ++i) {
                    var n = rnd.Next(rndLimit);
                    oList.Add(n);
                    //sortedList.Add(n);
                    //sortedList.Sort();
                    if(i % 1000 == 0) {
                        var temp = oList[i];
                        //if(oList[i] != sortedList[i]) {
                        //    Console.WriteLine("Failed");
                        //    return;
                        //}
                    }
                }
                SW.StopAndPrint("", z.ToString());
            }

            Console.WriteLine("Just add---");
            for(int z = 10; z <= limit; z *= 10) {
                var oList = new OrderedList<int>();
                SW.Restart("");
                for(int i = 0; i < z; ++i) {
                    oList.Add(rnd.Next(rndLimit));
                }
                SW.StopAndPrint("", z.ToString());
            }

            Console.WriteLine("Random access---");
            for(int z = 10; z <= limit; z *= 10) {
                var sortedList = new List<int>();
                var oList = new OrderedList<int>();
                for(int i = 0; i < z; ++i) {
                    var n = rnd.Next(rndLimit);
                    oList.Add(n);
                    sortedList.Add(n);
                }
                sortedList.Sort();
                SW.Restart("");
                for(int i = 0; i < z / 100; ++i) {
                    var index = rnd.Next(oList.Count - 1);
                    if(oList[index] != sortedList[index]) {
                        Console.WriteLine("Failed");
                        return;
                    }
                }
                SW.StopAndPrint("", z.ToString());
            }

            Console.WriteLine("Done!");
            Console.ReadKey();
        }

        static void PrintType<T1, T2>(Union<T1, T2> u) {
            u.Match(x => Console.WriteLine("T1"), x => Console.WriteLine("T2"));
        }

        static Union<T, Base> Test2<T>(Union<T, Base> val) {
            return val;
        }

        static void Test(Action<object> act) {
        }

        static int TestUnion2(object obj) {
            Console.WriteLine(obj);
            return 1;
        }

        static void TestUnion(Union<int, double> val) {
            val.Match(
                x => Console.WriteLine("int!!"),
                x => Console.WriteLine("double!!")
            );
        }
    }
}
