using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Encryptor.CommandReceiving
{
    public class ExternalReceiver
    {
        private TextWriter _writer;

        public ExternalReceiver(TextWriter writer)
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

            if (streamManager.ReadString() == "Some password string.")
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
