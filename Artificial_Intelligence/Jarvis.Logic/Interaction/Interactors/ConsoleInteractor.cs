namespace Jarvis.Logic.Interaction.Interactors
{
    using System;
    using Commons.Logger;
    using CommandControl;
    using Interfaces;

    public class ConsoleInteractor : IInteractor
    {
        private readonly ILogger _logger;
        private bool _isAlive = true;
        private bool _isActive = true;

        public ConsoleInteractor(ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentException($"Logger cannot be 0!");
            }

            this._logger = logger;

            Initialize();
        }

        public bool IsActive
        {
            get { return _isActive; }

        }

        public void SendOutput(string output, bool isAsync)
        {
            Console.WriteLine("  >Jarvis: " + output);
        }

        public void Start()
        {
            _isActive = true;
            while (_isAlive)
            {
                var command = Console.ReadLine();
                CommandContainer.Instance.AddCommand(_logger, command);
            }
        }

        public void Pause()
        {
            
        }

        public void Stop()
        {
            _isActive = false;
            _isAlive = false;
        }

        private void Initialize()
        {
            Console.Title = "Jarvis.Core";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
