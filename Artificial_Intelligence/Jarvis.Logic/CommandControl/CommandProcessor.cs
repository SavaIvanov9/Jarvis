using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Speech.Synthesis.TtsEngine;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jarvis.Commons.Utilities;
using Jarvis.Data;
using Jarvis.Logic.CommandControl.Constants;
using Jarvis.Logic.Interaction;
using Jarvis.Logic.Interaction.Interactors;
using Jarvis.Logic.Interaction.Interfaces;
using Jarvis.Logic.ProcessCommunication;
using Jarvis.RegistryEditor;
using Jarvis.SecureDesktop;
using Jarvis.Web;

namespace Jarvis.Logic.CommandControl
{
    public sealed class CommandProcessor
    {
        private static readonly Lazy<CommandProcessor> Lazy =
            new Lazy<CommandProcessor>(() => new CommandProcessor());

        private const string CommandNotFoundMsg = "Command not found.";
        private const string InvalidParametersMsg = "Invalid Parameters.";

        public static CommandProcessor Instance
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return Lazy.Value;
            }
        }

        public void Search(IList<string> commandParts, IList<string> commandParams, IInteractorManager interactor)
        {
            Validator.Instance.ValidateIsAboveOqEqualMinimum(commandParts.Count, 2, CommandNotFoundMsg);
            switch (commandParts[1])
            {
                case "web":
                    if (commandParams.Count > 0)
                    {
                        Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);
                        Validator.Instance.ValidateIsAboveOqEqualMinimum(commandParams.Count, 1, InvalidParametersMsg);
                        WebManager.Instance.WebSearch(commandParams);
                        interactor.SendOutput($@"Searching in web for ""{string.Join(" ", commandParams)}""");
                    }
                    else
                    {
                        Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);
                        WebManager.Instance.OpenSite(new List<string>() { "google.com" });
                        interactor.SendOutput($@"Searching in web.");
                    }
                    break;
                default:
                    interactor.SendOutput(CommandNotFoundMsg);
                    break;
            }
        }

        public void Open(IList<string> commandParts, IList<string> commandParams, IInteractorManager interactor)
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
                case "google":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    Process.Start("firefox.exe", "http://www.google.com");
                    interactor.SendOutput("Google opened.");
                    //secondProc.StartInfo;
                    //secondProc.Start();

                    //WebManager.Instance.OpenSite(new List<string>() {"google.com"});
                    //interactor.SendOutput($@"Google opened.");
                    break;
                case "youtube":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    Process.Start("firefox.exe", "http://www.youtube.com");
                    interactor.SendOutput("Youtube opened.");
                    //secondProc.StartInfo;
                    //secondProc.Start();

                    //WebManager.Instance.OpenSite(new List<string>() {"google.com"});
                    //interactor.SendOutput($@"Google opened.");
                    break;
                default:
                    interactor.SendOutput(CommandNotFoundMsg);
                    break;
            }
        }

        public void StartModule(IList<string> commandParts, IList<string> commandParams, IInteractorManager interactor)
        {
            Validator.Instance.ValidateIsAboveOqEqualMinimum(commandParts.Count, 2, CommandNotFoundMsg);
            switch (commandParts[1])
            {
                case ModuleName.SecureDesktop:
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    Process.Start(CommandConstants.SecureDesktopPath);
                    interactor.SendOutput("Securing password started.");
                    break;
                case ModuleName.Encryptor:
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    //ComunicationManager.Instance.StartServer(
                    //    ComunicationManager.Instance.AddServer("EncryptorPipe", "Some password"));

                    Process.Start(CommandConstants.EncryptorPath);
                    interactor.SendOutput("Encryptor strarted.");
                    break;
                case ModuleName.MovementDetection:
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    Process.Start(CommandConstants.MovementDetectionPath);
                    interactor.SendOutput("Movement detection strarted.");
                    break;
                default:
                    interactor.SendOutput(CommandNotFoundMsg);
                    break;
            }
        }

        //public void Close(IList<string> commandParts, IList<string> commandParams, IInteractorManager interactor)
        //{
        //    if(commandParts.Count > )
        //    switch (commandParts[1])
        //    {
        //        case "google":
        //            System.Windows.Forms.SendKeys("^w");
        //            break;

        //    }
        //    //System.Windows.Forms.SendKeys("^w")
        //}

        public void Close(IList<string> commandParts, IList<string> commandParams, IInteractorManager interactor)
        {
            Validator.Instance.ValidateIsAboveOqEqualMinimum(commandParts.Count, 2, CommandNotFoundMsg);
            switch (commandParts[1])
            {
                case ModuleName.Encryptor:
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);
                    //ComunicationManager.Instance.StopServer(0);
                    if (StopProcess(CommandConstants.EncryptorFile))
                    {
                        interactor.SendOutput("Encryptor closed.");
                    }
                    else
                    {
                        interactor.SendOutput("Encryptor process not found.");
                    }
                    break;

                case ModuleName.MovementDetection:
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    if (StopProcess(CommandConstants.MovementDetectionFile))
                    {
                        interactor.SendOutput("Movement detection closed.");
                    }
                    else
                    {
                        interactor.SendOutput("Movement detection process not found.");
                    }
                    break;

                case "gom":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    //foreach (var process in Process.GetProcesses())
                    //{
                    //    Console.WriteLine(process.ProcessName);
                    //}

                    if (StopProcess("GOM"))
                    {
                        interactor.SendOutput("GOM player closed.");
                    }
                    else
                    {
                        interactor.SendOutput("GOM player process not found.");
                    }
                    break;

                case "webtab":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);
                    SendKeys.SendWait("^w");
                    interactor.SendOutput("Tab closed.");
                    break;

            }
        }

        public void TellMe(IList<string> commandParts, IList<string> commandParams, IInteractorManager interactor)
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

        public void AddToStartup(IList<string> commandParts, IList<string> commandParams, IInteractorManager interactor)
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
        
        public void Gom(IList<string> commandParts, IList<string> commandParams, IInteractorManager interactorManager)
        {
            Validator.Instance.ValidateIsAboveOqEqualMinimum(commandParts.Count, 2, CommandNotFoundMsg);
            switch (commandParts[1])
            {
                case "pause":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);
                    KeySender.Instance.Send("gomplayer", new []{" "}, interactorManager);
                    //interactorManager.SendOutput("Gom paused");
                    break;
                case "back":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);
                    KeySender.Instance.Send("gomplayer", new[] { "{LEFT}" }, interactorManager);
                    //interactorManager.SendOutput("Gom back");
                    break;
                case "next":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);
                    KeySender.Instance.Send("gomplayer", new[] { "{RIGHT}" }, interactorManager);
                    //interactorManager.SendOutput("Gom next");
                    break;
            }
        }

        public void Shutup(IInteractorManager interactorManager)
        {
            foreach (var interactor in interactorManager.Interactors)
            {
                if (interactor.GetType() == typeof(VoiceInteractor))
                {
                    interactor.Pause();
                }
            }
        }

        public void StartProcess(string path)
        {
            Process secondProc = new Process();
            secondProc.StartInfo.FileName = path;
            secondProc.Start();
        }

        private bool StopProcess(string name)
        {
            bool result = false;
            foreach (var process in Process.GetProcessesByName(name))
            {
                process.Kill();
                result = true;
            }

            return result;
        }

        public void Exit(IInteractorManager interactorManager)
        {
            //ComunicationManager.Instance.StopAllServers();
            Shutup(interactorManager);
            interactorManager.SendOutput("See ya soon!", false);
            interactorManager.StopInteractors();
            Environment.Exit(0);
        }
    }
}
