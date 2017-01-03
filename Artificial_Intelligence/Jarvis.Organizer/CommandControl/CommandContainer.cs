namespace Jarvis.Organizer.CommandControl
{
    using System;
    using System.Collections.Generic;
    using Commons.Logger;

    public delegate void OnNewCommandHandler(string selectedValue);

    public class CommandContainer
    {
        public event OnNewCommandHandler OnNewCommand;

        private static readonly Lazy<CommandContainer> Lazy =
            new Lazy<CommandContainer>(() => new CommandContainer());

        private readonly IList<string> _commandList = new List<string>();
        private ILogger _logger;

        public static CommandContainer Instance => Lazy.Value;

        private void OnAdd(string value)
        {
            if (OnNewCommand != null)
            {
                OnNewCommand(value);
            }
        }

        public void Initialize(ILogger logger)
        {
            this._logger = logger;
        }

        public void AddCommand(string command)
        {
            _commandList.Add(command);
            _logger.LogCommand(command);
            OnAdd(command);
        }
    }
}
