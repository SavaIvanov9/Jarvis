using System;
using System.Collections.Generic;
using Jarvis.Commons.Logger;

namespace Jarvis.Logic.CommandControl
{
    public delegate void OnNewCommandHandler(string selectedValue);

    public class CommandContainer
    {
        public event OnNewCommandHandler OnNewCommand;

        private static readonly Lazy<CommandContainer> Lazy =
            new Lazy<CommandContainer>(() => new CommandContainer());

        public static CommandContainer Instance => Lazy.Value;

        private void OnAdd(string value)
        {
            if (OnNewCommand != null)
            {
                OnNewCommand(value);
            }
        }

        private IList<string> CommandList = new List<string>();

        public void AddCommand(ILogger logger, string command)
        {
            CommandList.Add(command);
            logger.Log(command);
            OnAdd(command);
        }
    }
}
