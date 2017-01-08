using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Commons.Logger;
using Jarvis.Organizer.CommandReceiving;
using Jarvis.Organizer.Output;

namespace Jarvis.Organizer.CommandControl
{
    public class CommandManager
    {
        private static readonly Lazy<CommandManager> Lazy =
            new Lazy<CommandManager>(() => new CommandManager());
        private ILogger _logger;
        private IOutputManager _outputManager;
        private IReceiverManager _receiverManager;
        private const string CommandNotFoundMsg = "Command not found.";
        private const string InvalidParametersMsg = "Invalid Parameters.";

        private CommandManager()
        {
            CommandContainer.Instance.OnNewCommand += ManageCommand;
        }

        public static CommandManager Instance
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get { return Lazy.Value; }
        }

        public void Initialize(ILogger logger, IOutputManager outputManager, IReceiverManager receiverManager)
        {
            this._logger = logger;
            this._outputManager = outputManager;
            this._receiverManager = receiverManager;
            //ManageCommand(CommandConstants.Initialize);
        }

        public void ManageCommand(string command)
        {
            try
            {
                if (!string.IsNullOrEmpty(command))
                {
                    //if (Encryptor.Commands.EncryptorConstants.ValidChoises.Contains(command))
                    //{
                    //    KeySender.Instance.Send(CommandConstants.EncryptorFile, new []{command}, _interactorManager);
                    //}

                    //var commandSegments = ParseInput(command);
                    //IList<string> commandParts = commandSegments.Item1;
                    //IList<string> commandParams = commandSegments.Item2;

                    switch (command)
                    {
                        //case CommandConstants.Initialize:
                        //    CommandProcessor.Instance.Initialize(_interactorManager);
                        //    break;

                        case CommandConstants.StartSleepRecording:
                            CommandProcessor.Instance.StartSleepRecording(_outputManager, _logger);
                            break;

                        case CommandConstants.StoptSleepRecording:
                            CommandProcessor.Instance.StoptSleepRecording(_outputManager, _logger);
                            break;

                        case CommandConstants.GetSleepData:
                            CommandProcessor.Instance.GetSleepStatistic(_outputManager, _logger);
                            break;

                        case CommandConstants.AddEvent:
                            CommandProcessor.Instance.AddEvent(_outputManager, _logger);
                            break;

                        case CommandConstants.GetEventsData:
                            CommandProcessor.Instance.GetEventsData(_outputManager, _logger);
                            break;

                        case CommandConstants.Exit:
                            CommandProcessor.Instance.Exit(_receiverManager);
                            break;

                        default:
                            _logger.Log(CommandNotFoundMsg);
                            break;
                    }
                }
                else
                {
                    _logger.Log("Command cannot be empty!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogCommand(ex.ToString());
            }
        }
    }
}
