﻿using System;
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

            //if (args.Length > 0)
            //{
            //    using (PipeStream pipeClient =
            //        new AnonymousPipeClientStream(PipeDirection.In, args[0]))
            //    {
            //        Console.WriteLine("[CLIENT] Current TransmissionMode: {0}.",
            //           pipeClient.TransmissionMode);

            //        using (StreamReader sr = new StreamReader(pipeClient))
            //        {
            //            // Display the read text to the console
            //            string temp;

            //            // Wait for 'sync message' from the server.
            //            do
            //            {
            //                Console.WriteLine("[CLIENT] Wait for sync...");
            //                temp = sr.ReadLine();
            //            }
            //            while (!temp.StartsWith("SYNC"));

            //            // Read the server data and echo to the console.
            //            while ((temp = sr.ReadLine()) != null)
            //            {
            //                Console.WriteLine("[CLIENT] Echo: " + temp);
            //            }
            //        }
            //    }
            //}
            //Console.ReadLine();

            var comClient = new CommunicationClient(Console.Out);
            comClient.Start();
        }
    }
}
