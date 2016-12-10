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
        private NamedPipeServerStream _pipeServer;
        private StreamManager ss;

        public CommunicationServer(string serverName, string connectionPassword, OnExit onExit)
        {
            this._serverName = serverName;
            this._connectionPassword = connectionPassword;
            //onExit += ExitServer;
            ComunicationManager.Instance.ExitServer += ExitServer;
        }

        private void ExitServer()
        {
            //ss.WriteString("stop connection to server");
            Console.WriteLine($"{_serverName} server closed.");
            
            _pipeServer.Dispose();
        }

        public void Start()
        {
            Console.WriteLine("Server started. Waiting for clients to connect...");

            _pipeServer = new NamedPipeServerStream(
                _serverName, PipeDirection.InOut);

            int threadId = Thread.CurrentThread.ManagedThreadId;

            _pipeServer.WaitForConnection();
            
            try
            {
                // Read the request from the client. Once the client has
                // written to the pipe its security token will be available.
                ss = new StreamManager(_pipeServer);

                Console.WriteLine($"Client[{ss.ReadString()}] connected to server[{_serverName}] on thread[{threadId}].");

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
            _pipeServer.Close();
        }

        private void OnAsyncMessage(IAsyncResult result)
        {
            Int32 bytesRead = _pipeServer.EndRead(result);
            if (bytesRead != 0)
            {
                // good times -- process the message
            }
            else
            {
                // pipe disconnected, right?!? NOPE!
                _pipeServer.Close();
                throw new Exception();
            }

        }
    }
}
