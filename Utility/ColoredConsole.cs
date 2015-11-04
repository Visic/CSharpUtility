using System;

namespace Utility
{
    public static class ColoredConsole
    {
        public static void WriteLine(ConsoleColor color, string msg)
        {
            DoWithColor(color, () => Console.WriteLine(msg));
        }

        public static void WriteLine(ConsoleColor color, string formatString, params object[] args)
        {
            DoWithColor(color, () => Console.WriteLine(formatString, args));
        }

        public static void Write(ConsoleColor color, string msg)
        {
            DoWithColor(color, () => Console.Write(msg));
        }

        public static void Write(ConsoleColor color, string formatString, params object[] args)
        {
            DoWithColor(color, () => Console.Write(formatString, args));
        }

        private static void DoWithColor(ConsoleColor color, Action work)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            work();
            Console.ForegroundColor = currentColor;
        }
    }
}
