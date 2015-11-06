using System;

namespace Utility {
    public static class Logger {
        static ApplicationErrorLog _errorLog;

        static string _applicationLoggerPath;
        public static string ApplicationLoggerPath {
            get { return _applicationLoggerPath; }
            set {
                _applicationLoggerPath = value;
                _errorLog = new ApplicationErrorLog(_applicationLoggerPath);
            }
        }

        public static string ApplicationDisplayName { get; set; } = "";

        public static void RegisterContext(string fieldName, string information) {
            if(_errorLog != null) _errorLog.RegisterContext(fieldName, information);
        }

        public static void LogException(Exception ex) {
            if(_errorLog != null) _errorLog.LogException(ex);
            ColoredConsole.WriteLine(ConsoleColor.Red, $"{ApplicationDisplayName}: Exception: {ex.GetType()} -- {ex.Message}");
        }

        public static void LogError(string msg) {
            if(_errorLog != null) _errorLog.LogMessage(msg);
            ColoredConsole.WriteLine(ConsoleColor.Red, $"{ApplicationDisplayName}: {msg}");
        }

        public static void LogError(string formatString, params object[] args) {
            if(_errorLog != null) _errorLog.LogMessage(string.Format(formatString, args));
            ColoredConsole.WriteLine(ConsoleColor.Red, $"{ApplicationDisplayName}: {formatString}", args);
        }

        public static void LogInformation(string msg) {
            Console.WriteLine($"{ApplicationDisplayName}: {msg}");
        }

        public static void LogInformation(string formatString, params object[] args) {
            Console.WriteLine($"{ApplicationDisplayName}: {formatString}", args);
        }

        public static void LogDebug(string msg) {
            ColoredConsole.WriteLine(ConsoleColor.Green, $"{ApplicationDisplayName}: {msg}");
        }

        public static void LogDebug(string formatString, params object[] args) {
            ColoredConsole.WriteLine(ConsoleColor.Green, $"{ApplicationDisplayName}: {formatString}", args);
        }
    }
}
