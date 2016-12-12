using System.IO;
using System.IO.Pipes;
using System.Security.Principal;
using Jarvis.Encryptor.Commands;

namespace Jarvis.Encryptor.ProcessCommunication
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
                        "EncryptorPipe",
                        PipeDirection.InOut,
                        PipeOptions.None,
                        TokenImpersonationLevel.Impersonation);

            //_writer.WriteLine("Waiting for connection to server for external commands...\n");
            pipeClient.Connect();
            _writer.WriteLine($"Established connection to server for external commands.");

            StreamManager streamManager = new StreamManager(pipeClient, _writer);
            streamManager.WriteString("Jarvis.Encryptor");

            if (streamManager.ReadString() == "Some password")
            {
                try
                {
                    while (_command != EncryptorConstants.Exit || _command != "stop connection to server")
                    {
                        if (pipeClient.IsConnected)
                        {
                            this._command = streamManager.ReadString();
                            CommandContainer.Instance.AddCommand(_command, _writer);
                            //_writer.WriteLine(_command);
                        }
                        else
                        {
                            _writer.WriteLine("Server closed.");
                            pipeClient.Dispose();
                            pipeClient.Close();
                        }
                    }
                    //streamManager.StartListeningForNewCommand();

                }
                catch (IOException e)
                {
                    _writer.WriteLine("ERROR: {0}", e.Message);
                }
                pipeClient.Close();
            }
            else
            {
                _writer.WriteLine("Server could not be verified.");
            }

            pipeClient.Dispose();
            pipeClient.Close();
            _writer.WriteLine("Client closed.");
        }
    }
}
