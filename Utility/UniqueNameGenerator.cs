using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Utility {
    public static class UniqueNameGenerator {
        public static string NextNumbered(string targetName, IEnumerable<string> currentNames) {
            var fileNamePattern = $@"^{targetName}\((\d*?)\)$";

            var nextNumber = 0;
            var currentNumbers = currentNames.Select(x => Path.GetFileNameWithoutExtension(x))
                                       .Select(x => {
                                           var groups = Regex.Match(x, fileNamePattern).Groups;
                                           return groups.Count > 1 ? int.Parse(groups[1].Value) : -1;
                                       }).ToArray();

            if(currentNumbers.Length > 0) nextNumber = currentNumbers.Max(x => x) + 1;
            return $"{targetName}({nextNumber})";
        }
    }
}
