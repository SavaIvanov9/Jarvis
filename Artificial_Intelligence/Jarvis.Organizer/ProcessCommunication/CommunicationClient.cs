using System;
using System.IO;
using System.IO.Pipes;
using System.Security.Principal;
using System.Threading;

namespace Jarvis.Organizer.ProcessCommunication
{
    public class CommunicationClient
    {
        private readonly TextWriter _writer;
        private string _command = "";

        public CommunicationClient(TextWriter writer)
        {
            this._writer = writer;
        }

        public void Start()
        {
            NamedPipeClientStream pipeClient = new NamedPipeClientStream(
                        ".",
                        "Jarvis.Core.Organizer",
                        PipeDirection.InOut,
                        PipeOptions.None,
                        TokenImpersonationLevel.Impersonation);

            _writer.WriteLine("Connecting to server...\n");
            pipeClient.Connect();
            _writer.WriteLine("Connected to server");

            StreamManager streamManager = new StreamManager(pipeClient);
            streamManager.WriteString("Jarvis.Organizer");

            if (streamManager.ReadString() == "Some password")
            {
                try
                {
                    this._command = streamManager.ReadString();
                    _writer.WriteLine(_command);

                    while (_command != "exit")
                    {
                        this._command = streamManager.ReadString();
                        _writer.WriteLine(_command);
                    }
                }
                catch (IOException e)
                {
                    _writer.WriteLine("ERROR: {0}", e.Message);
                }
                //pipeClient.Dispose();
                //pipeClient.Close();
            }
            else
            {
                _writer.WriteLine("Server could not be verified.");
            }

            //pipeClient.Dispose();
            pipeClient.Close();
            _writer.WriteLine("Client closed.");
            Thread.Sleep(4000);
        }
    }
}
