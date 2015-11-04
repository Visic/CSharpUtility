using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Utility
{
    public static class UniqueFileNameGenerator
    {
        public static string NextNumbered(string fileName) {
            return NextNumbered(Directory.EnumerateFiles(Path.GetDirectoryName(fileName)), fileName);
        }

        private static string NextNumbered(IEnumerable<string> fileNames, string targetName) {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(targetName);
            var fileNamePattern = string.Format("^{0}{1}$", fileNameWithoutExtension, @"\((\d*?)\)");

            var nextNumber = 0;
            var fileNumbers = fileNames.Select(x => Path.GetFileNameWithoutExtension(x))
                                       .Select(x => {
                                           var groups = Regex.Match(x, fileNamePattern).Groups;
                                           return groups.Count > 1 ? int.Parse(groups[1].Value) : -1; 
                                       }).ToArray();

            if (fileNumbers.Length > 0) nextNumber = fileNumbers.Max(x => x) + 1;

            var fullPathWithoutExtension = Path.Combine(Path.GetDirectoryName(targetName), fileNameWithoutExtension);
            return string.Format("{0}({1}){2}", fullPathWithoutExtension, nextNumber, Path.GetExtension(targetName));
        }
    }
}
