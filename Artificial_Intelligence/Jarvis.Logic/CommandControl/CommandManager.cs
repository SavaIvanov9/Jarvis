
namespace Jarvis.Logic.CommandControl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using Commons.CrashReporter;
    using Commons.Logger;
    using Commons.Utilities;
    using Constants;
    using Interaction.Interfaces;
    
    public sealed class CommandManager
    {
        private static readonly Lazy<CommandManager> Lazy =
            new Lazy<CommandManager>(() => new CommandManager());

        private ILogger _logger;
        private IInteractorManager _interactorManager;
        private IReporter _reporter;
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

        public void Start(IInteractorManager interactorManager, ILogger logger, IReporter reporter)
        {
            this._interactorManager = interactorManager;
            this._logger = logger;
            this._reporter = reporter;
            ManageCommand(CommandConstants.Initialize);
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

                    var commandSegments = ParseInput(command);
                    IList<string> commandParts = commandSegments.Item1;
                    IList<string> commandParams = commandSegments.Item2;

                    switch (commandParts[0])
                    {
                        case CommandConstants.Initialize:
                            CommandProcessor.Instance.Initialize(_interactorManager, _logger);
                            break;

                        case CommandConstants.AddToStartup:
                            CommandProcessor.Instance.AddToStartup(commandParts, commandParams,
                                _interactorManager);
                            break;

                        case CommandConstants.Tell:
                            CommandProcessor.Instance.TellMe(commandParts, commandParams,
                                _interactorManager, _logger);
                            break;

                        case CommandConstants.StartModule:
                            CommandProcessor.Instance.StartModule(
                                commandParts, commandParams, _interactorManager, _logger);
                            break;

                        case CommandConstants.Close:
                            CommandProcessor.Instance.Close(commandParts, commandParams,
                                _interactorManager);
                            break;

                        case CommandConstants.Stop:
                            CommandProcessor.Instance.Stop(commandParts, commandParams,
                                _interactorManager, _logger);
                            break;

                        case CommandConstants.Open:
                            CommandProcessor.Instance.Open(commandParts, commandParams,
                                _interactorManager);
                            break;

                        case CommandConstants.Search:
                            CommandProcessor.Instance.Search(commandParts, commandParams,
                                _interactorManager);
                            break;

                        case CommandConstants.Shutup:
                            CommandProcessor.Instance.Shutup(_interactorManager);
                            break;

                        case CommandConstants.Mute:
                            CommandProcessor.Instance.Mute(_interactorManager);
                            _interactorManager.SendOutput("Voice interaction paused.", false);
                            break;

                        case CommandConstants.UnMute:
                            CommandProcessor.Instance.UnMute(_interactorManager);
                            _interactorManager.SendOutput("Voice interaction unpaused.", false);
                            break;

                        case CommandConstants.Exit:
                            CommandProcessor.Instance.Exit(_interactorManager);
                            break;

                        case CommandConstants.Show:
                            Utility.Instance.Show();
                            _interactorManager.SendOutput("Command window moved to front.", false);
                            break;

                        case CommandConstants.Hide:
                            Utility.Instance.Hide();
                            _interactorManager.SendOutput("Command window moved to background", false);
                            break;

                        case CommandConstants.Gom:
                            CommandProcessor.Instance.Gom(commandParts, commandParams,
                                _interactorManager);
                            break;

                        case CommandConstants.Help:
                            CommandProcessor.Instance.Help(commandParts, commandParams,
                                _interactorManager, _logger);
                            break;

                        case "nexttab":
                            SendKeys.SendWait("^{TAB}");
                            _interactorManager.SendOutput("Moved to next tab.", false);
                            break;

                        case "previoustab":
                            SendKeys.SendWait("^+{TAB}");
                            _interactorManager.SendOutput("Moved to previous tab.", false);
                            break;

                        default:
                            _interactorManager.SendOutput(CommandNotFoundMsg);
                            break;
                    }
                }
                else
                {
                    _interactorManager.SendOutput("Command cannot be empty!", false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCommand(ex.Message 
                    + Environment.NewLine + _reporter.CreateReport(ex));
            }
        }

        public Tuple<IList<string>, IList<string>> ParseInput(string inputLine)
        {
            IList<string> commandSegments = inputLine
                .Split(new[] { ": " }, StringSplitOptions.None)
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
    }
}
