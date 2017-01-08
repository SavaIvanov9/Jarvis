namespace Jarvis.Logic.CommandControl
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using Commons.Logger;
    using Commons.Utilities;
    using Data;
    using Commons.Exceptions;
    using Constants;
    using Interaction.Interactors;
    using Interaction.Interfaces;
    using ProcessCommunication;
    using RegistryEditor;
    using Web;

    public sealed class CommandProcessor
    {
        private static readonly Lazy<CommandProcessor> Lazy =
            new Lazy<CommandProcessor>(() => new CommandProcessor());

        private const string CommandNotFoundMsg = "Command not found.";
        private const string InvalidParametersMsg = "Invalid Parameters.";

        public static CommandProcessor Instance
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get { return Lazy.Value; }
        }

        public void Initialize(IInteractorManager interactorManager, ILogger logger)
        {
            interactorManager.SendOutput("Jarvis core system started.");
            foreach (var interactor in interactorManager.Interactors)
            {
                logger.Log($"{interactor.GetType().Name} activated.");
            }

            try
            {
                var db = new JarvisData();
                db.SleepTimes.All().ToList().Count();
                logger.Log($"Connection to database {db.GetType().Name} established.");
            }
            catch (Exception)
            {
                logger.Log($"Failed to connect to database.");
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
                case "mail":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    Process.Start("firefox.exe", "http://mail.google.com/");

                    interactor.SendOutput("Mail opened.");
                    break;

                case "git":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    //Process.Start("C:\\Users\\savat\\AppData\\Local\\GitHub\\GitHub.appref-ms");
                    Process.Start("C:\\Users\\savat\\AppData\\Local\\Apps\\2.0\\E821A0P3.QBQ\\R8NDHZWJ.D1B\\gith..tion_317444273a93ac29_0003.0003_665ccbdbd3c2d8d4\\GitHub.exe");
                    interactor.SendOutput("Git Hub opened.");
                    break;

                case "sqlserver":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    Process.Start("ssms.exe");

                    interactor.SendOutput("MS SQL Server Management Studio opened.");
                    break;

                case "visualstudio":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);
                    Process.Start("devenv.exe");
                    interactor.SendOutput("Visual Studio opened.");
                    break;

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

                case "projects":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    Process.Start("C:\\Users\\savat\\Documents\\Projects");
                    interactor.SendOutput("Projects opened.");
                    break;

                case "documents":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    Process.Start("C:\\Users\\savat\\Documents");
                    interactor.SendOutput("Documents opened.");
                    break;

                default:
                    interactor.SendOutput(CommandNotFoundMsg);
                    break;
            }
        }

        public void Start(IList<string> commandParts, IList<string> commandParams,
            IInteractorManager interactor, ILogger logger)
        {
            Validator.Instance.ValidateIsAboveOqEqualMinimum(commandParts.Count, 2, CommandNotFoundMsg);
            switch (commandParts[1])
            {
                case ModuleConstants.SecureDesktop:
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    Process.Start(CommandConstants.SecureDesktopPath);
                    interactor.SendOutput("Securing password started.");
                    break;
                case ModuleConstants.Encryptor:
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    //ComunicationManager.Instance.StartServer(
                    //    ComunicationManager.Instance.AddServer("EncryptorPipe", "Some password"));

                    Process.Start(CommandConstants.EncryptorPath);
                    interactor.SendOutput("Encryptor strarted.");
                    break;

                case ModuleConstants.MovementDetection:
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    Process.Start(CommandConstants.MovementDetectionPath);
                    interactor.SendOutput("Movement detection strarted.");
                    break;

                case ModuleConstants.Organizer:
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    try
                    {
                        int index = CommunicationManager.Instance.AddServer("Jarvis.Core.Organizer", "Some password",
                        logger, interactor);

                        CommunicationManager.Instance.StartServer(index);
                        Process.Start(CommandConstants.OrganizerPath);
                        interactor.SendOutput("Organizer strarted.");
                    }
                    catch (DuplicateInstanceException ex)
                    {
                        interactor.SendOutput(ex.Message);
                    }
                    break;

                case "sleeprecording":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    CommunicationContainer.Instance.AddMessage(ModuleConstants.StartSleepRecording);

                    logger.Log("Sleep secording started.");
                    break;

                case ModuleConstants.GetRedyTimeTracker:
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    CommunicationContainer.Instance.AddMessage("getreadytime");

                    interactor.SendOutput("getreadytime started.");
                    break;

                case "mail":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    Process.Start("firefox.exe", "http://mail.google.com/");

                    interactor.SendOutput("Mail opened.");
                    break;

                case "git":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    //Process.Start("C:\\Users\\savat\\AppData\\Local\\GitHub\\GitHub.appref-ms");
                    Process.Start("C:\\Users\\savat\\AppData\\Local\\Apps\\2.0\\E821A0P3.QBQ\\R8NDHZWJ.D1B\\gith..tion_317444273a93ac29_0003.0003_665ccbdbd3c2d8d4\\GitHub.exe");
                    interactor.SendOutput("Git Hub opened.");
                    break;

                case "sqlserver":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    Process.Start("ssms.exe");

                    interactor.SendOutput("MS SQL Server Management Studio opened.");
                    break;

                case "visualstudio":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);
                    Process.Start("devenv.exe");
                    interactor.SendOutput("Visual Studio opened.");
                    break;

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

                case "projectsfolder":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    Process.Start("C:\\Users\\savat\\Documents\\Projects");
                    interactor.SendOutput("Projects opened.");
                    break;

                case "documentsfolder":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    Process.Start("C:\\Users\\savat\\Documents");
                    interactor.SendOutput("Documents opened.");
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

        public void Close(IList<string> commandParts, IList<string> commandParams,
            IInteractorManager interactor, ILogger logger)
        {
            Validator.Instance.ValidateIsAboveOqEqualMinimum(commandParts.Count, 2, CommandNotFoundMsg);
            switch (commandParts[1])
            {
                case ModuleConstants.Encryptor:
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

                case ModuleConstants.MovementDetection:
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

                case ModuleConstants.Organizer:
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    try
                    {
                        CommunicationContainer.Instance.AddMessage("exit");
                        CommunicationManager.Instance.StopServer("Jarvis.Core.Organizer");
                        interactor.SendOutput("Organizer closed.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        interactor.SendOutput("Organizer process not found.");
                    }

                    break;

                case "media":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    //foreach (var process in Process.GetProcesses())
                    //{
                    //    Console.WriteLine(process.ProcessName);
                    //}

                    if (StopProcess("GOM"))
                    {
                        interactor.SendOutput("media player closed.");
                    }
                    else
                    {
                        interactor.SendOutput("media player process not found.");
                    }
                    break;

                case "tab":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);
                    SendKeys.SendWait("^w");
                    interactor.SendOutput("Tab closed.");
                    break;

                case "sleeprecording":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    CommunicationContainer.Instance.AddMessage(ModuleConstants.StoptSleepRecording);

                    logger.Log("Sleep secording stopped.");
                    break;

                default:
                    interactor.SendOutput(CommandNotFoundMsg);
                    break;
            }
        }

        public void TellMe(IList<string> commandParts, IList<string> commandParams,
            IInteractorManager interactor, ILogger logger)
        {
            Validator.Instance.ValidateIsAboveOqEqualMinimum(
                commandParts.Count, 2, CommandNotFoundMsg);
            switch (commandParts[1])
            {
                case "sleepdata":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);

                    CommunicationContainer.Instance.AddMessage(ModuleConstants.GetSleepStatistic);

                    logger.Log("Sleep statistic started.");
                    //interactor.SendOutput("Sleep statistic started.");
                    break;
                case "random":
                    Validator.Instance.ValidateIsAboveOqEqualMinimum(
                        commandParts.Count, 3, CommandNotFoundMsg);
                    switch (commandParts[2])
                    {
                        case "number":
                            //Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count,
                            //    3, CommandNotFoundMsg);
                            //Validator.Instance.ValidateIsAboveOqEqualMinimum(commandParams.Count,
                            //    2, InvalidParametersMsg);

                            if (commandParams.Count == 0)
                            {
                                //Mute(interactor);
                                interactor.SendOutput(
                                    Utility.Instance.RandomNumber().ToString());
                                //UnMute(interactor);
                            }
                            else
                            {
                                //Mute(interactor);
                                interactor.SendOutput(
                                    Utility.Instance.RandomNumber(int.Parse(commandParams[0]),
                                    int.Parse(commandParams[1])).ToString());
                                //UnMute(interactor);
                            }

                            break;

                        case "string":
                            //Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count,
                            //    3, CommandNotFoundMsg);
                            //Validator.Instance.ValidateIsAboveOqEqualMinimum(commandParams.Count,
                            //    2, InvalidParametersMsg);
                            if (commandParams.Count == 0)
                            {
                                logger.Log(Utility.Instance.RandomString());
                            }
                            else
                            {
                                logger.Log(
                                    Utility.Instance.RandomString(int.Parse(commandParams[0]),
                                    int.Parse(commandParams[1])));
                            }

                            break;

                        case "date":
                            //Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count,
                            //    3, CommandNotFoundMsg);
                            //Validator.Instance.ValidateIsAboveOqEqualMinimum(commandParams.Count,
                            //    2, InvalidParametersMsg);
                            if (commandParams.Count == 0)
                            {
                                interactor.SendOutput(
                                    Utility.Instance.RandomDateTime().ToString("d"));
                            }
                            else
                            {
                                interactor.SendOutput(Utility.Instance.RandomDateTime(
                                    DateTime.Parse(commandParams[0]),
                                    DateTime.Parse(commandParams[1]))
                                    .ToString("d"));
                            }

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
                                0, FakeDb.Instance.Jokes.Count - 1)], true);
                    break;

                default:
                    interactor.SendOutput(CommandNotFoundMsg);
                    break;
            }
        }

        public void AddToStartup(IList<string> commandParts, IList<string> commandParams, IInteractorManager interactor)
        {
            Validator.Instance.ValidateIsAboveOqEqualMinimum(commandParts.Count, 1, CommandNotFoundMsg);
            //Validator.Instance.ValidateIsAboveOqEqualMinimum(commandParams.Count, 1, InvalidParametersMsg);
            switch (commandParts[1])
            {
                case "yourself":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);
                    RegistryEditorModule.Instance.AddProcessToStartup("Jarvis.Client.exe");
                    interactor.SendOutput("Jarvis added to startup.");
                    break;
                default:
                    interactor.SendOutput(CommandNotFoundMsg);
                    break;
            }
        }

        public void Help(IList<string> commandParts, IList<string> commandParams,
            IInteractorManager interactorManager, ILogger logger)
        {

            interactorManager.SendOutput("Available commands are:", true);

            foreach (var command in CommandConstants.AllCommands)
            {
                logger.Log("  " + command);
            }
            //interactorManager.SendOutput(string.Join(Environment.NewLine, CommandConstants.AllCommands));
        }

        public void Player(IList<string> commandParts, IList<string> commandParams, IInteractorManager interactorManager)
        {
            Validator.Instance.ValidateIsAboveOqEqualMinimum(commandParts.Count, 2, CommandNotFoundMsg);
            switch (commandParts[1])
            {
                case "pause":
                    Validator.Instance.ValidateIsUnderOrEqualMax(commandParts.Count, 2, CommandNotFoundMsg);
                    KeySender.Instance.Send("gomplayer", new[] { " " }, interactorManager);
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
                    //interactor.SendOutput("", true);
                }
            }
        }

        public void Mute(IInteractorManager manager)
        {
            manager.Interactors.Find(x => x.GetType() == typeof(VoiceInteractor)).Stop();
            //manager.SendOutput("Voice interaction paused.");
        }

        public void UnMute(IInteractorManager manager)
        {
            var voiceInteractor = manager.Interactors.Find(x => x.GetType() == typeof(VoiceInteractor));
            if (!voiceInteractor.IsActive)
            {
                voiceInteractor.Start();
                //manager.SendOutput("Voice interaction unpaused.");
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
            interactorManager.SendOutput("See ya soon!");
            interactorManager.StopInteractors();
            Environment.Exit(0);
        }
    }
}
