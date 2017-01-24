using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NamedPipes.Client.Common;

namespace NamedPipes.Client
{
    class JarvisClient
    {
        private int numClients = 1;
        private string command = "";
        
        public void Start()
        {
            NamedPipeClientStream pipeClient = new NamedPipeClientStream(
                        ".",
                        "testpipe",
                        PipeDirection.InOut,
                        PipeOptions.None,
                        TokenImpersonationLevel.Impersonation);
            
            Console.WriteLine("Connecting to server...\n");
            pipeClient.Connect();
            Console.WriteLine($"Connected to server");

            StreamString ss = new StreamString(pipeClient);

            if (ss.ReadString() == "Some password string.")
            {
                //string message = ss.ReadString();
                //while (message != "end")
                //{
                //    Console.WriteLine(message);
                //    message = ss.ReadString();
                //}

                StartListeningForNewCommand(ss);
            }
            else
            {
                Console.WriteLine("Server could not be verified.");
            }
            
            pipeClient.Close();
            Console.WriteLine("Client closed.");
        }

        public void StartListeningForNewCommand(StreamString ss)
        {
            //new Thread(() =>
            //{
                while (command != "end")
                {
                    this.command = ss.ReadString();
                    Console.WriteLine(command);
                }
            //}).Start();
        }

        // Helper function to create pipe client processes
        private void StartClients()
        {
            int i;
            string currentProcessName = Environment.CommandLine;
            Process[] plist = new Process[numClients];

            Console.WriteLine("Spawning client processes...\n");

            if (currentProcessName.Contains(Environment.CurrentDirectory))
            {
                currentProcessName = currentProcessName.Replace(Environment.CurrentDirectory, String.Empty);
            }

            // Remove extra characters when launched from Visual Studio
            currentProcessName = currentProcessName.Replace("\\", String.Empty);
            currentProcessName = currentProcessName.Replace("\"", String.Empty);

            for (i = 0; i < numClients; i++)
            {
                // Start 'this' program but spawn a named pipe client.
                plist[i] = Process.Start(currentProcessName, "spawnclient");
            }
            while (i > 0)
            {
                for (int j = 0; j < numClients; j++)
                {
                    if (plist[j] != null)
                    {
                        if (plist[j].HasExited)
                        {
                            Console.WriteLine("Client process[{0}] has exited.",
                                plist[j].Id);
                            plist[j] = null;
                            i--; // decrement the process watch count
                        }
                        else
                        {
                            Thread.Sleep(250);
                        }
                    }
                }
            }
            Console.WriteLine("\nClient processes finished, exiting.");
        }
    }
}
