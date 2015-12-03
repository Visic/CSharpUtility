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
