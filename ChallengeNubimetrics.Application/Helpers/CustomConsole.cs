using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeNubimetrics.Application.Helpers
{
    public static class Printer
    {
        public static void Print(string message, ConsoleColor backgroundColor = default, ConsoleColor foregroundColor = default)
        {
            Console.ForegroundColor = foregroundColor != default ? foregroundColor : default;
            Console.BackgroundColor = backgroundColor != default ? backgroundColor : default;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
