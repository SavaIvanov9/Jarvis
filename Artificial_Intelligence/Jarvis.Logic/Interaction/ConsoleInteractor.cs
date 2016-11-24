using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Jarvis.Commons.Logger;
using Jarvis.Logic.CommandControl;
using Jarvis.Logic.Interaction.Interfaces;

namespace Jarvis.Logic.Interaction
{
    public class ConsoleInteractor : IInteractor
    {
        private readonly ILogger _logger;

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

        public Tuple<IList<string>, IList<string>>  ParseInput(string inputLine)
        {
            IList<string> commandSegments = inputLine
                .Split(new [] { ": "}, StringSplitOptions.None)
                .ToList();

            IList<string> commandParts = commandSegments[0]
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            if (commandSegments.Count > 1)
            {
                IList<string> commandParams = commandSegments[1]
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

                return new Tuple<IList<string>, IList<string>>(commandParts, commandParams);
            }

            return new Tuple<IList<string>, IList<string>>(commandParts, new List<string>());
        }

        public void SendOutput(string output)
        {
            Console.WriteLine("  >Jarvis: " + output);
        }

        public void Start()
        {
            var command = Console.ReadLine();
            CommandContainer.Instance.AddCommand(_logger, command);
        }

        public void Stop()
        {
            
        }
    }
}
