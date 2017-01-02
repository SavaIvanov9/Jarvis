namespace Jarvis.Logic.ProcessCommunication
{
    using System.IO;
    using System.IO.Pipes;
    using System.Threading;
    using Commons.Logger;
    using Interaction.Interfaces;

    public class CommunicationServer
    {
        private readonly string _serverName;
        private readonly string _connectionPassword;
        private readonly ILogger _logger;
        private readonly IInteractorManager _manager;
        private NamedPipeServerStream _pipeServer;
        private StreamManager _stream;
        private bool _isAlive = true;

        public CommunicationServer(string serverName, string connectionPassword,
            ILogger logger, IInteractorManager manager)
        {
            this._serverName = serverName;
            this._connectionPassword = connectionPassword;
            this._logger = logger;
            this._manager = manager;
            CommunicationManager.Instance.ExitServer += ExitServer;
            CommunicationContainer.Instance.OnNewMessage += SendMessage;
        }

        private void ExitServer()
        {
            CommunicationContainer.Instance.OnNewMessage -= SendMessage;
            _isAlive = false;
            _pipeServer.Close();
            //_logger.Log($"{_serverName} server closed.");
        }

        public void Start()
        {
            _pipeServer = new NamedPipeServerStream(
                _serverName,
                PipeDirection.InOut);

            int threadId = Thread.CurrentThread.ManagedThreadId;

            _logger.Log($"[{_serverName}] server started. Waiting for clients to connect...");
            _pipeServer.WaitForConnection();
            try
            {
                // Read the request from the client. Once the client has
                // written to the pipe its security token will be available.
                _stream = new StreamManager(_pipeServer);

                _logger.Log($"Client [{_stream.ReadString()}] connected to [{_serverName}] server on thread[{threadId}].");

                // Verify our identity to the connected client using a
                // string that the client anticipates.
                _stream.WriteString(_connectionPassword);

                while (_isAlive)
                {
                    
                }
            }
            // Catch the IOException that is raised if the pipe is broken
            // or disconnected.
            catch (IOException e)
            {
                _logger.Log($"ERROR: {e.Message}");
            }
            _logger.Log($"{_serverName} server closed.");
            _pipeServer.Close();
        }

        public void SendMessage(string message)
        {
            _stream.WriteString(message);
        }
    }
}
