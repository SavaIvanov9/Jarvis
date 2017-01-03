namespace Jarvis.Organizer
{
    using System;
    using System.Runtime.CompilerServices;
    using Commons.Logger;
    using CommandControl;
    using CommandReceiving;
    using CommandReceiving.Receivers;
    using Output;

    public class OrganizerEngine
    {
        private readonly ILogger _logger;
        private readonly IReceiverManager _receiverManager;
        private readonly IOutputManager _outputManager;
        
        private OrganizerEngine()
        {
            this._logger = new ConsoleLogger();
            this._receiverManager = new ReceiverManager();
            this._outputManager = new OutputManager(_logger); 
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static OrganizerEngine Instance()
        {
            return new OrganizerEngine();
        }

        public void Start()
        {
            Console.Title = "Jarvis.Organizer";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;

            CommandContainer.Instance.Initialize(_logger);
            CommandManager.Instance.Initialize(_logger, _outputManager, _receiverManager);

            _receiverManager.AddReceiver(new ConsoleReceiver());
            _receiverManager.AddReceiver(new JarvisCoreReceiver(_logger));
            _receiverManager.StartReceivers();
        }
    }
}
