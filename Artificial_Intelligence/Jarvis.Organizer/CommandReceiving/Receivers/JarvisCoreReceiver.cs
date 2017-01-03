namespace Jarvis.Organizer.CommandReceiving.Receivers
{
    using System.IO;
    using System.IO.Pipes;
    using System.Security.Principal;
    using System.Threading;
    using Commons.Logger;
    using CommandControl;
    using ProcessCommunication;

    public class JarvisCoreReceiver : IReceiver
    {
        private readonly ILogger _logger;
        private string _command = "";
        private bool _isActive = true;

        public JarvisCoreReceiver(ILogger logger)
        {
            this._logger = logger;
        }

        public void Start()
        {
            NamedPipeClientStream pipeClient = new NamedPipeClientStream(
                        ".",
                        "Jarvis.Core.Organizer",
                        PipeDirection.InOut,
                        PipeOptions.None,
                        TokenImpersonationLevel.Impersonation);

            _logger.Log("Connecting to Jarvis.Core...\n");
            pipeClient.Connect();
            _logger.Log("Connected to Jarvis.Core established.\n");

            StreamManager streamManager = new StreamManager(pipeClient);
            streamManager.WriteString("Jarvis.Organizer");

            if (streamManager.ReadString() == "Some password")
            {
                try
                {
                    //this._command = streamManager.ReadString();

                    while (true)
                    {
                        if (_isActive == false)
                        {
                            break;
                        }
                        this._command = streamManager.ReadString();
                        CommandContainer.Instance.AddCommand(_command);
                    }

                    //CommandContainer.Instance.AddCommand(_command);
                }
                catch (IOException e)
                {
                    _logger.Log($"ERROR: {e.Message}");
                }
            }
            else
            {
                _logger.Log("Server could not be verified.");
            }

            pipeClient.Close();
            _logger.Log("Client closed.");
        }

        public void Stop()
        {
            _isActive = false;
        }
    }
}
