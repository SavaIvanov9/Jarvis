using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace Jarvis.Logic.ProcessCommunication
{
    public class CommunicationServer
    {
        private readonly string _serverName;
        private readonly string _connectionPassword;
        private NamedPipeServerStream pipeServer;

        public CommunicationServer(string serverName, string connectionPassword, OnExit onExit)
        {
            this._serverName = serverName;
            this._connectionPassword = connectionPassword;
            //onExit += ExitServer;
            ComunicationManager.Instance.ExitServer += ExitServer;
        }

        private void ExitServer()
        {
            Console.WriteLine("izlizai beeee");
            pipeServer.Close();
        }

        public void Start()
        {
            Console.WriteLine("Server started. W8ing for clients...");

            pipeServer = new NamedPipeServerStream(
                _serverName, PipeDirection.InOut);

            int threadId = Thread.CurrentThread.ManagedThreadId;

            // Wait for a client to connect
            //new Thread((() =>
            //{
            pipeServer.WaitForConnection();

            //})).Start();

            //new Thread((() =>
            //{
            //    int len = 0;
            //    len = pipeServer.ReadByte() * 256;
            //    len += pipeServer.ReadByte();
            //    byte[] inBuffer = new byte[len];
                
                
            //    while (true)
            //    {
            //        try
            //        {
            //            pipeServer.BeginRead(inBuffer, 0, len, OnAsyncMessage, null);

            //            //if (pipeServer.IsConnected)
            //            //{

            //            //}
            //            //else
            //            //{
            //            //    throw new Exception("hui");
            //            //}
            //        }
            //        catch (Exception)
            //        {
            //            Console.WriteLine("server closed maika ti da eba");
            //            pipeServer.Close();
            //            break;
            //        }

            //        //if (pipeServer.IsConnected)
            //        //{

            //        //}
            //        //else
            //        //{
            //        //    pipeServer.Close();
            //        //    Console.WriteLine("server closed");
            //        //    break;
            //        //}
            //    }
            //})).Start();

            Console.WriteLine($"Client [{pipeServer.GetImpersonationUserName()}] connected on thread[{threadId}].");
            try
            {
                // Read the request from the client. Once the client has
                // written to the pipe its security token will be available.
                StreamManager ss = new StreamManager(pipeServer);

                // Verify our identity to the connected client using a
                // string that the client anticipates.
                ss.WriteString(_connectionPassword);

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
            Console.WriteLine("closing ur ass");
            pipeServer.Close();
        }

        private void OnAsyncMessage(IAsyncResult result)
        {
            Int32 bytesRead = pipeServer.EndRead(result);
            if (bytesRead != 0)
            {
                // good times -- process the message
            }
            else
            {
                // pipe disconnected, right?!? NOPE!
                pipeServer.Close();
                throw new Exception();
            }

        }
    }
}
