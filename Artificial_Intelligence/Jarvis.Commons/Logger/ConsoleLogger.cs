using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Commons.Logger
{
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
