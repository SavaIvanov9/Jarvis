using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NamedPipes.Commons;

namespace NamedPipes
{
    public class JarvisServer
    {
        public void Start()
        {
            Console.WriteLine("Server started. W8ing for clients...");

            NamedPipeServerStream pipeServer = new NamedPipeServerStream(
                "testpipe", PipeDirection.InOut);

            int threadId = Thread.CurrentThread.ManagedThreadId;

            // Wait for a client to connect
            pipeServer.WaitForConnection();
            
            Console.WriteLine($"Client [{pipeServer.GetImpersonationUserName()}] connected on thread[{threadId}]." );
            try
            {
                // Read the request from the client. Once the client has
                // written to the pipe its security token will be available.
                StreamString ss = new StreamString(pipeServer);

                // Verify our identity to the connected client using a
                // string that the client anticipates.
                ss.WriteString("Some password string.");
               
                string message = Console.ReadLine();
                
                while (message != "end")
                {
                    ss.WriteString(message);
                    message = Console.ReadLine();
                }

                ss.WriteString(message);
            }
            // Catch the IOException that is raised if the pipe is broken
            // or disconnected.
            catch (IOException e)
            {
                Console.WriteLine("ERROR: {0}", e.Message);
            }
            pipeServer.Close();
        }
    }
}
