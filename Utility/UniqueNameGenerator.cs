using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Utility {
    public static class UniqueNameGenerator {
        public static string NextNumbered(string targetName, IEnumerable<string> currentNames) {
            var currentNumbers = GetCurrentNumbers(targetName, currentNames);
            var nextNumber = currentNumbers.Count() > 0 ? currentNumbers.Max() + 1 : 0;
            return $"{targetName}({nextNumber})";
        }

        public static string FirstAvailableNumbered(string targetName, IEnumerable<string> currentNames) {
            var currentNumbers = GetCurrentNumbers(targetName, currentNames);
            var nextNumber = currentNumbers.Count() > 0 ? GetFirstSequenceGap(currentNumbers) : 0;
            return $"{targetName}({nextNumber})";
        }

        private static IEnumerable<int> GetCurrentNumbers(string targetName, IEnumerable<string> currentNames) {
            var fileNamePattern = $@"^{targetName}\((\d*?)\)$";

            return currentNames.Select(x => Path.GetFileNameWithoutExtension(x))
                               .Select(x => {
                                   var groups = Regex.Match(x, fileNamePattern).Groups;
                                   return groups.Count > 1 ? int.Parse(groups[1].Value) : -1;
                               }).ToArray();
        }

        private static int GetFirstSequenceGap(IEnumerable<int> seq) {
            var currentMax = seq.Max();
            var fullSeq = Methods.CreateSequence<int>(
                x => x.Match<Option<int>>(
                    y => y > currentMax ? new Option<int>() : Option.New(y + 1), 
                    y => 0
                )
            );

            return fullSeq.Except(seq).First();
        }
    }
}
