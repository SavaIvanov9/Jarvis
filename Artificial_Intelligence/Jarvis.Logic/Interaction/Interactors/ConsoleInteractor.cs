using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Jarvis.Commons.Logger;
using Jarvis.Logic.CommandControl;
using Jarvis.Logic.Interaction.Interfaces;

namespace Jarvis.Logic.Interaction.Interactors
{
    public class ConsoleInteractor : IInteractor
    {
        private readonly ILogger _logger;
        private bool _isAlive = true;

        public ConsoleInteractor(ILogger logger)
        {
            if (logger == null)
            {
                throw new InvalidEnumArgumentException($"Logger cannot be 0!");
            }

            this._logger = logger;

            Console.Title = "Jarvis";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void SendOutput(string output, bool isAsync)
        {
            Console.WriteLine("  >Jarvis: " + output);
        }

        public void Start()
        {
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
            _isAlive = false;
        }
    }
}
