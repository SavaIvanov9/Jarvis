namespace Jarvis.Logic.CommandControl
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

        public static CommandContainer Instance => Lazy.Value;
        
        private void OnAdd(string value)
        {
            if (OnNewCommand != null)
            {
                OnNewCommand(value);
            }
        }
        
        public void AddCommand(ILogger logger, string command)
        {
            _commandList.Add(command);
            logger.LogCommand(command);
            OnAdd(command);
        }
    }
}
