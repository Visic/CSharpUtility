using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Utility {
    public class ApplicationErrorLog {
        const string C_DirectoryPath = "ExceptionLogs";
        object _lockObject = new object();
        Dictionary<string, string> _errorContext = new Dictionary<string, string>();
        Func<Exception, IEnumerable<string>> _customExceptionHandler;
        int _logFileLimit;

        public ApplicationErrorLog(string rootPath = null, int logFileLimit = -1) {
            _logFileLimit = logFileLimit;
            ExceptionLogFolderPath = rootPath != null ? Path.Combine(rootPath, C_DirectoryPath) : C_DirectoryPath;
            if(!Directory.Exists(ExceptionLogFolderPath)) Directory.CreateDirectory(ExceptionLogFolderPath);
            _customExceptionHandler = e => new string[0];
        }

        public ApplicationErrorLog(Func<Exception, IEnumerable<string>> customExceptionHandler, string rootPath = null)
            : this(rootPath) {
            _customExceptionHandler = customExceptionHandler;
        }

        public string ExceptionLogFolderPath { get; private set; }

        public void RegisterContext(string fieldName, string information) {
            lock (_lockObject) {
                _errorContext[fieldName] = information;
            }
        }

        public void LogMessage(string message) {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Message logged at: " + DateTime.Now.ToString());
            AppendContextString(stringBuilder);
            stringBuilder.AppendLine("Message: " + message);

            lock (_lockObject) {
                WriteFile(stringBuilder.ToString());
            }
        }

        public void LogException(Exception e) {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Exception occurred at: " + DateTime.Now.ToString());
            AppendContextString(stringBuilder);
            LogException_Rec(e, stringBuilder);

            lock (_lockObject) {
                WriteFile(stringBuilder.ToString());
            }
        }

        private void WriteFile(string content) {
            if(_logFileLimit > -1) {
                var files = Directory.EnumerateFiles(ExceptionLogFolderPath);
                if(files.Count() >= _logFileLimit) {
                    var fileInfos = files.Select(x => new FileInfo(x)).OrderBy(x => x.CreationTimeUtc);
                    var filesToDelete = fileInfos.Take(files.Count() - _logFileLimit + 1);
                    foreach(var fileToDelete in filesToDelete) {
                        fileToDelete.Delete();
                    }
                }
            }

            var currentLogs = Directory.EnumerateFiles(ExceptionLogFolderPath);
            var fileName = UniqueNameGenerator.NextNumbered("ExceptionLog", currentLogs);
            File.WriteAllText(Path.Combine(ExceptionLogFolderPath, fileName) + ".txt", content);
        }

        //recursive exception logging helper method
        private void LogException_Rec(Exception e, StringBuilder stringBuilder) {
            stringBuilder.AppendLine("Source: " + e.Source);
            stringBuilder.AppendLine("Message: " + e.Message);
            stringBuilder.AppendLine("Exception type: " + e.GetType().ToString());

            foreach(var extraLine in _customExceptionHandler(e)) {
                stringBuilder.AppendLine(extraLine);
            }

            stringBuilder.AppendLine("Stack trace: \n" + e.StackTrace);

            if(e.InnerException != null) {
                stringBuilder.AppendLine("\nInner exception:");
                LogException_Rec(e.InnerException, stringBuilder);
            }
        }

        private void AppendContextString(StringBuilder stringBuilder) {
            foreach(var kvp in _errorContext) {
                stringBuilder.AppendFormat("{0}: {1}\n", kvp.Key, kvp.Value);
            }
            stringBuilder.AppendLine();
        }
    }
}
