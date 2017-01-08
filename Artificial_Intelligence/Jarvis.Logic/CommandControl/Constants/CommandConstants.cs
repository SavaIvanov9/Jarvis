namespace Jarvis.Logic.CommandControl.Constants
{
    internal class CommandConstants
    {
        public const string Initialize = "init";
        public const string AddToStartup = "addtostartup";
        public const string Tell = "tellme";
        public const string Start = "start";
        public const string Open = "open";
        public const string Search = "search";
        public const string Close = "close";
        public const string Stop = "stop";
        public const string Shutup = "shutup";
        public const string Show = "gotofront";
        public const string Hide = "gotobackground";
        public const string Exit = "exit";
        public const string Player = "player";
        public const string Mute = "silence";
        public const string UnMute = "enablevoice";
        public const string Help = "showcommands";

        public static readonly string[] AllCommands =
        {
            "open encryptor",
            "start encryptor",
            "close encryptor",

            "open securedpass",
            "start securedpass",

            "open movementdetection",
            "start movementdetection",
            "close movementdetection",

            "open organizer",
            "start organizer",
            "close organizer",

            "open sleeprecording",
            "start sleeprecording",
            "close sleeprecording",

            "tellme eventstoday",
            "tellme sleepdata",
            //"start getredytime",

            "start mail",
            "open mail",

            "start visualstudio",
            "open visualstudio",

            "start sqlserver",
            "open sqlserver",

            "start git",
            "open git",

            "start google",
            "open google",

            "start youtube",
            "open youtube",

            "start projectsfolder",
            "open projectsfolder",

            "start documentsfolder",
            "open documentsfolder",

            "nexttab",
            "previoustab",

            "close tab",
            "close media",

            "tellme joke",
            "tellme random number",
            "tellme random string",
            "tellme random date",

            "showcommands",
            "shutup",
            "silence",
            "enablevoice",

            "addtostartup yourself",
            "gotobackground",
            "gotofront",
            "exit",

            "player pause",
            "player back",
            "player next"
        };

        public const string SecureDesktopPath =
            "..\\..\\..\\Jarvis.SecureDesktop\\bin\\Debug\\Jarvis.SecureDesktop.exe";
        public const string EncryptorPath =
            "..\\..\\..\\Jarvis.Encryptor\\bin\\Debug\\Jarvis.Encryptor.exe";
        public const string MovementDetectionPath =
            "..\\..\\..\\Jarvis.MovementDetection\\bin\\Debug\\Jarvis.MovementDetection.exe";
        public const string OrganizerPath =
            "..\\..\\..\\Jarvis.Organizer\\bin\\Debug\\Jarvis.Organizer.exe";
        
        public const string SecureDesktopFile = "Jarvis.SecureDesktop";
        public const string EncryptorFile = "Jarvis.Encryptor";
        public const string MovementDetectionFile = "Jarvis.MovementDetection";
        public const string OrganizerFile = "Jarvis.Organizer";
    }
}
