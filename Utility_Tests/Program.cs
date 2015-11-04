using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Utility_Tests
{
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

    class Program
    {
        static void Main(string[] args)
        {

            Console.ReadKey();
        }

        static Union<T, Base> Test2<T>(Union<T, Base> val)
        {
            return val;
        }

        static Union<T, Base> Test1<T>(T val)
        {
            return Union<T, Base>.Create(val);
        }

        static Union<T, Base> Test1<T>(Base val)
        {
            return Union<T, Base>.Create(val);
        }

        static void Test(Action<object> act)
        {
        }

        static int TestUnion2(object obj)
        {
            Console.WriteLine(obj);
            return 1;
        }

        static void TestUnion(Union<int, double> val)
        {
            val.Match(
                x => Console.WriteLine("int!!"),
                x => Console.WriteLine("double!!")
            );
        }
    }
}
