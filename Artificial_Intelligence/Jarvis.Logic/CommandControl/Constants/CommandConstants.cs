namespace Jarvis.Logic.CommandControl.Constants
{
    internal class CommandConstants
    {
        public const string Initialize = "init";
        public const string AddToStartup = "addtostartup";
        public const string Tell = "tellme";
        public const string StartModule = "start";
        public const string Open = "open";
        public const string Search = "search";
        public const string Close = "close";
        public const string Stop = "stop";
        public const string Shutup = "shutup";
        public const string Show = "gotofront";
        public const string Hide = "gotobackground";
        public const string Exit = "exit";
        public const string Gom = "media";
        public const string Mute = "mute";
        public const string UnMute = "unmute";
        public const string Help = "help";

        public static readonly string[] AllCommands =
        {
            "start encryptor",
            "close encryptor",
            "start securedpass",
            "start movementdetection",
            "close movementdetection",

            "start organizer",
            "start sleeprecording",
            "stop sleeprecording",
            "tellme sleepdata",
            "start getredytime",
            "close organizer",

            "open mail",
            "open visualstudio",
            "open sqlserver",
            "open git",
            "open google",
            "open youtube",
            "open projects",
            "open documents",
            "nexttab",
            "previoustab",

            "close tab",
            "close media",

            "tellme joke",
            "tellme random number",
            "tellme random string",
            "tellme random number",

            "help",
            "shutup",
            "mute",
            "unmute",

            "addtostartup yourself",
            "gotobackground",
            "gotofront",
            //"stop",
            "exit",

            "media pause",
            "media back",
            "media next"
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
