using System.IO;
using System.IO.Pipes;
using System.Security.Principal;

namespace Jarvis.Encryptor.ProcessCommunication
{
    public class CommunicationClient
    {
        private TextWriter _writer;

        public CommunicationClient(TextWriter writer)
        {
            this._writer = writer;
        }

        public void Start()
        {
            NamedPipeClientStream pipeClient = new NamedPipeClientStream(
                        ".",
                        "EncryptorPipe",
                        PipeDirection.InOut,
                        PipeOptions.None,
                        TokenImpersonationLevel.Impersonation);

            //_writer.WriteLine("Waiting for connection to server for external commands...\n");
            pipeClient.Connect();
            _writer.WriteLine($"Established connection to server for external commands.");

            StreamManager streamManager = new StreamManager(pipeClient, _writer);

            if (streamManager.ReadString() == "Some password")
            {
                streamManager.StartListeningForNewCommand();
            }
            else
            {
                _writer.WriteLine("Server could not be verified.");
            }

            pipeClient.Close();
            _writer.WriteLine("Client closed.");
        }
    }
}
