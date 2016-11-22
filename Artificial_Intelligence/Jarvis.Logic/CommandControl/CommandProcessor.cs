using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Jarvis.Commons.Utilities;
using Jarvis.Data;
using Jarvis.Logic.Interaction;
using Jarvis.RegistryEditor;
using Jarvis.SecureDesktop;
using Jarvis.Web;

namespace Jarvis.Logic.CommandControl
{
    public sealed class CommandProcessor
    {
        private static readonly Lazy<CommandProcessor> Lazy =
            new Lazy<CommandProcessor>(() => new CommandProcessor());

        private InteractorManager _interactorManager;
        private const string CommandNotFoundMsg = "Command not found.";
        private const string InvalidParametersMsg = "Invalid Parameters.";

        private CommandProcessor()
        {
            CommandContainer.Instance.OnNewCommand += ProcessCommand;
        }

        public static CommandProcessor Instance => Lazy.Value;

        public void Start(InteractorManager interactorManager)
        {
            this._interactorManager = interactorManager;
            ProcessCommand(CommandConstants.Initialize);
        }

        public void ProcessCommand(string command)
        {
            if (!string.IsNullOrEmpty(command))
            {
                var commandSegments = ParseInput(command);
                IList<string> commandParts = commandSegments.Item1;
                IList<string> commandParams = commandSegments.Item2;

                switch (commandParts[0])
                {
                    case CommandConstants.Initialize:
                        _interactorManager.SendOutput("Hi, I am Jarvis.");
                        break;
                    case CommandConstants.AddToStartup:
                        AddToStartup(commandParts, commandParams, _interactorManager);
                        break;
                    case CommandConstants.Tell:
                        TellMe(commandParts, commandParams, _interactorManager);
                        break;
                    case CommandConstants.StartModule:
                        StartModule(commandParts, commandParams, _interactorManager);
                        break;
                    case CommandConstants.Stop:
                        StopProcess(commandParts, commandParams, _interactorManager);
                        break;
                    case CommandConstants.Open:
                        Open(commandParts, commandParams, _interactorManager);
                        break;
                    case CommandConstants.Search:
                        Search(commandParts, commandParams, _interactorManager);
                        break;
                    case CommandConstants.Exit:
                        _interactorManager.SendOutput("See you.");
                        Environment.Exit(0);
                        break;
                    default:
                        _interactorManager.SendOutput(CommandNotFoundMsg);
                        break;
                }
            }
            else
            {
                _interactorManager.SendOutput("Command cannot be empty!");
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

        public void Search(IList<string> commandParts, IList<string> commandParams, InteractorManager interactor)
        {
            Validator.Instance.ValidateIsAboveOqEqualMinimum(commandParts.Count, 2, CommandNotFoundMsg);
            switch (commandParts[1])
            {
                case "web":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);
                    Validator.Instance.ValidateIsAboveOqEqualMinimum(commandParams.Count, 1, InvalidParametersMsg);
                    WebManager.Instance.WebSearch(commandParams);
                    interactor.SendOutput($@"Seraching in web for ""{string.Join(" ", commandParams)}""");
                    break;
                default:
                    interactor.SendOutput(CommandNotFoundMsg);
                    break;
            }
        }

        public void Open(IList<string> commandParts, IList<string> commandParams, InteractorManager interactor)
        {
            Validator.Instance.ValidateIsAboveOqEqualMinimum(commandParts.Count, 2, CommandNotFoundMsg);
            switch (commandParts[1])
            {
                case "site":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);
                    Validator.Instance.ValidateIsAboveOqEqualMinimum(commandParams.Count, 1, InvalidParametersMsg);
                    WebManager.Instance.OpenSite(commandParams);
                    interactor.SendOutput($"{commandParams[0]} opened with Firefox.");
                    break;
                default:
                    interactor.SendOutput(CommandNotFoundMsg);
                    break;
            }
        }

        public void StartModule(IList<string> commandParts, IList<string> commandParams, InteractorManager interactor)
        {
            Validator.Instance.ValidateIsAboveOqEqualMinimum(commandParts.Count, 2, CommandNotFoundMsg);
            switch (commandParts[1])
            {
                case ModuleName.SecureDesktop:
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);
                    SecureDesktopModule.Instance.Start();
                    interactor.SendOutput("Password saved to clipboard.");
                    break;
                case ModuleName.Encryptor:
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    Process secondProc = new Process();
                    secondProc.StartInfo.FileName = GlobalConstants.EncryptorPath;
                    secondProc.Start();
                    
                    break;
                default:
                    interactor.SendOutput(CommandNotFoundMsg);
                    break;
            }
        }

        private void StopProcess(IList<string> commandParts, IList<string> commandParams, InteractorManager interactor)
        {
            Validator.Instance.ValidateIsAboveOqEqualMinimum(commandParts.Count, 2, CommandNotFoundMsg);
            switch (commandParts[1])
            {
                case ModuleName.Encryptor:
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);
                    foreach (var process in Process.GetProcessesByName("Jarvis.Encryptor"))
                    {
                        process.Kill();
                    }
                    break;
            }
        }

        private void TellMe(IList<string> commandParts, IList<string> commandParams, InteractorManager interactor)
        {
            Validator.Instance.ValidateIsAboveOqEqualMinimum(
                commandParts.Count, 2, CommandNotFoundMsg);
            switch (commandParts[1])
            {
                case "random":
                    Validator.Instance.ValidateIsAboveOqEqualMinimum(
                        commandParts.Count, 3, CommandNotFoundMsg);
                    switch (commandParts[2])
                    {
                        case "number":
                            Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count,
                                3, CommandNotFoundMsg);
                            Validator.Instance.ValidateIsAboveOqEqualMinimum(commandParams.Count,
                                2, InvalidParametersMsg);
                            interactor.SendOutput(
                                Utility.Instance.RandomNumber(
                                int.Parse(commandParams[0]),
                                int.Parse(commandParams[1]))
                                .ToString());
                            break;
                        case "string":
                            Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count,
                                3, CommandNotFoundMsg);
                            Validator.Instance.ValidateIsAboveOqEqualMinimum(commandParams.Count,
                                2, InvalidParametersMsg);
                            interactor.SendOutput(
                            Utility.Instance.RandomString(
                                int.Parse(commandParams[0]),
                                int.Parse(commandParams[1])));
                            break;
                        case "date":
                            Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count,
                                3, CommandNotFoundMsg);
                            Validator.Instance.ValidateIsAboveOqEqualMinimum(commandParams.Count,
                                2, InvalidParametersMsg);
                            interactor.SendOutput(
                            Utility.Instance.RandomDateTime(
                                DateTime.Parse(commandParams[0]),
                                DateTime.Parse(commandParams[1]))
                                .ToString("d"));
                            break;
                        default:
                            interactor.SendOutput(CommandNotFoundMsg);
                            break;
                    }
                    break;
                case "joke":
                    Validator.Instance.ValidateIsUnderOrEqualMax(
                        commandParts.Count, 2, CommandNotFoundMsg);
                    interactor.SendOutput(
                        FakeDb.Instance.Jokes[
                            Utility.Instance.RandomNumber(
                                0, FakeDb.Instance.Jokes.Count - 1)]);
                    break;
                default:
                    interactor.SendOutput(CommandNotFoundMsg);
                    break;
            }
        }

        private void AddToStartup(IList<string> commandParts, IList<string> commandParams, InteractorManager interactor)
        {
            Validator.Instance.ValidateIsAboveOqEqualMinimum(commandParts.Count, 1, CommandNotFoundMsg);
            Validator.Instance.ValidateIsAboveOqEqualMinimum(commandParams.Count, 1, InvalidParametersMsg);
            switch (commandParams[0])
            {
                case "jarvis":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);
                    RegistryEditorModule.Instance.AddProcessToStartup("Jarvis.Client.exe");
                    interactor.SendOutput("Jarvis added to startup.");
                    break;
                default:
                    interactor.SendOutput(CommandNotFoundMsg);
                    break;
            }
        }


    }
}
