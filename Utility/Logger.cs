using System;

namespace Utility {
    public static class Logger {
        static ApplicationErrorLog _errorLog;
        static object _lockObject = new object();

        static string _applicationLoggerPath;
        public static string ApplicationLoggerPath {
            get { return _applicationLoggerPath; }
            set {
                _applicationLoggerPath = value;
                _errorLog = new ApplicationErrorLog(_applicationLoggerPath);
            }
        }

        public static ConsoleColor ColorForExceptions { get; set; } = ConsoleColor.Red;
        public static ConsoleColor ColorForErrors { get; set; } = ConsoleColor.Red;
        public static ConsoleColor ColorForInformation { get; set; } = ConsoleColor.White;
        public static ConsoleColor ColorForDebug { get; set; } = ConsoleColor.DarkGreen;
        public static ConsoleColor ColorForTimestamp { get; set; } = ConsoleColor.White;
        public static ConsoleColor ColorForAppName { get; set; } = ConsoleColor.White;
        public static ConsoleColor ColorForSperator { get; set; } = ConsoleColor.White;
        public static string TimestampFormat { get; set; } = "";
        public static string ApplicationDisplayName { get; set; } = "";

        public static void RegisterContext(string fieldName, string information) {
            if(_errorLog != null) _errorLog.RegisterContext(fieldName, information);
        }

        public static void LogException(Exception ex) {
            if(_errorLog != null) _errorLog.LogException(ex);
            WriteAtomic(() => ColoredConsole.WriteLine(ColorForExceptions, $"Exception: {ex.GetType()} -- {ex.Message}"));
        }

        public static void LogError(string msg) {
            if(_errorLog != null) _errorLog.LogMessage(msg);
            WriteAtomic(() => ColoredConsole.WriteLine(ColorForErrors, msg));
        }

        public static void LogError(string formatString, params object[] args) {
            if(_errorLog != null) _errorLog.LogMessage(string.Format(formatString, args));
            WriteAtomic(() => ColoredConsole.WriteLine(ColorForErrors, formatString, args));
        }

        public static void LogInformation(string msg) {
            WriteAtomic(() => ColoredConsole.WriteLine(ColorForInformation, msg));
        }

        public static void LogInformation(string formatString, params object[] args) {
            WriteAtomic(() => ColoredConsole.WriteLine(ColorForInformation, formatString, args));
        }

        public static void LogDebug(string msg) {
            WriteAtomic(() => ColoredConsole.WriteLine(ColorForDebug, msg));
        }

        public static void LogDebug(string formatString, params object[] args) {
            WriteAtomic(() => ColoredConsole.WriteLine(ColorForDebug, formatString, args));
        }

        private static void WriteAtomic(Action writeMessage) {
            lock (_lockObject) {
                ColoredConsole.Write(ColorForTimestamp, $"[{DateTime.Now.ToString(TimestampFormat)}]");
                if(!string.IsNullOrEmpty(ApplicationDisplayName)) ColoredConsole.Write(ColorForAppName, $" {ApplicationDisplayName}");
                ColoredConsole.Write(ColorForSperator, ": ");
                writeMessage();
            }
        }
    }
}
