using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Organizer.ProcessCommunication;

namespace Jarvis.Organizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Jarvis Organizer";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            
            var comClient = new CommunicationClient(Console.Out);
            comClient.Start();
        }
    }
}
