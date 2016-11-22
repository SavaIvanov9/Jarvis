using System;
using System.Collections.Generic;
using System.Diagnostics;
using Jarvis.Commons.Utilities;
using Jarvis.Data;
using Jarvis.Logic.Interaction;
using Jarvis.Logic.Interaction.Interfaces;
using Jarvis.RegistryEditor;
using Jarvis.SecureDesktop;
using Jarvis.Web;

namespace Jarvis.Logic.CommandControl
{
    public sealed class CommandProcessor
    {
        private static readonly Lazy<CommandProcessor> Lazy =
            new Lazy<CommandProcessor>(() => new CommandProcessor());

        private IInteractor _interactor = new ConsoleInteractor();
        private const string CommandNotFoundMsg = "Command not found.";
        private const string InvalidParametersMsg = "Invalid Parameters.";

        private CommandProcessor()
        {
            CommandContainer.Instance.OnNewCommand += ProcessCommand;
        }

        public static CommandProcessor Instance => Lazy.Value;

        public void Start(IInteractor interactor)
        {
            this._interactor = interactor;
            ProcessCommand(CommandConstants.Initialize);
        }

        public void ProcessCommand(string command)
        {
            var commandSegments = _interactor.ParseInput(command);
            IList<string> commandParts = commandSegments.Item1;
            IList<string> commandParams = commandSegments.Item2;

            switch (commandParts[0])
            {
                case CommandConstants.Initialize:
                    _interactor.SendOutput(" >Jarvis: Hi, I am Jarvis");
                    break;
                case CommandConstants.AddToStartup:
                    AddToStartup(commandParts, commandParams, _interactor);
                    break;
                    //return true;
                case CommandConstants.Tell:
                    TellMe(commandParts, commandParams, _interactor);
                    break;
                    //return true;
                case CommandConstants.StartModule:
                    StartModule(commandParts, commandParams, _interactor);
                    break;
                    //return true;
                case CommandConstants.Open:
                    Open(commandParts, commandParams, _interactor);
                    break;
                    //return true;
                case CommandConstants.Search:
                    Search(commandParts, commandParams, _interactor);
                    break;
                    //return true;
                case CommandConstants.Exit:
                    _interactor.SendOutput(" >Jarvis: See ya ;)");
                    Environment.Exit(0);
                    break;
                    //return false;
                default:
                    _interactor.SendOutput(CommandNotFoundMsg);
                    break;
                    //return true;
            }
        }

        public void Search(IList<string> commandParts, IList<string> commandParams, IInteractor interactor)
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

        public void Open(IList<string> commandParts, IList<string> commandParams, IInteractor interactor)
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

        public void StartModule(IList<string> commandParts, IList<string> commandParams, IInteractor interactor)
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

                    //foreach (var process in Process.GetProcessesByName("Jarvis.Encryptor"))
                    //{
                    //    process.Kill();
                    //}

                    break;
                default:
                    interactor.SendOutput(CommandNotFoundMsg);
                    break;
            }
        }

        private void TellMe(IList<string> commandParts, IList<string> commandParams, IInteractor interactor)
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
                        MockedDb.Instance.Jokes[
                            Utility.Instance.RandomNumber(
                                0, MockedDb.Instance.Jokes.Count - 1)]);
                    break;
                default:
                    interactor.SendOutput(CommandNotFoundMsg);
                    break;
            }
        }

        private void AddToStartup(IList<string> commandParts, IList<string> commandParams, IInteractor interactor)
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
