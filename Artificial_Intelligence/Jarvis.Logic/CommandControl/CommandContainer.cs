using System;
using System.Collections.Generic;

namespace Jarvis.Logic.CommandControl
{
    public delegate void OnNewCommandHandler(string selectedValue);

    public class CommandContainer
    {
        public event OnNewCommandHandler OnNewCommand;

        private static readonly Lazy<CommandContainer> Lazy =
            new Lazy<CommandContainer>(() => new CommandContainer());

        private CommandContainer()
        {
        }

        public static CommandContainer Instance => Lazy.Value;

        private void OnAdd(string value)
        {
            if (OnNewCommand != null)
            {
                OnNewCommand(value);
            }
        }

        private IList<string> CommandList = new List<string>();

        public void AddCommand(string command)
        {
            CommandList.Add(command);
            OnAdd(command);
        }
    }
}
