using System;

namespace FuryTech.OdataTypescriptServiceGenerator
{
    public class Logger
    {
        public static void Log(string logEntry)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{DateTime.Now} - {logEntry}");
        }

        public static void Error(string logEntry)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{DateTime.Now} - {logEntry}");
        }

        public static void Warning(string logEntry)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{DateTime.Now} - {logEntry}");
        }

    }
}
