using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Encryptor.Commands
{
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

        public void AddCommand(string command, TextWriter writer)
        {
            _commandList.Add(command);
            writer.WriteLine(command);
            OnAdd(command);
        }
    }
}
