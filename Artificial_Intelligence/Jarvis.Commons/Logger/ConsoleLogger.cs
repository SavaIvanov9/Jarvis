namespace Jarvis.Commons.Logger
{
    using System;

    public class ConsoleLogger : ILogger
    {
        public void LogCommand(string message)
        {
            Console.WriteLine("Command: " + message);
        }

        public void Log(string message)
        {
            Console.WriteLine("  >Jarvis: " + message);
        }
    }
}
