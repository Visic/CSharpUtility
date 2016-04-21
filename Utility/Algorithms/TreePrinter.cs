using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility {
    public static class TreePrinter {
        public static string ToString<T>(T root, Func<T, IEnumerable<T>> getChildren) {
            return ToString_Rec(root, getChildren, "", new StringBuilder()).ToString();
        }

        private static StringBuilder ToString_Rec<T>(T cur, Func<T, IEnumerable<T>> getChildren, string prefix, StringBuilder acc) {
            acc.AppendLine(string.Format("{0}{1}", prefix, cur.ToString()));

            var children = getChildren(cur);
            for(int i = 0; i < children.Count(); ++i) {
                ToString_Rec(children.ElementAt(i), getChildren, prefix.Replace('+', '|') + "+ ", acc);
            }

            return acc;
        }
    }
}
