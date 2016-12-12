namespace Jarvis.Logic.CommandControl.Constants
{
    internal class CommandConstants
    {
        public const string Initialize = "init";
        public const string AddToStartup = "addtostartup";
        public const string Tell = "tell";
        public const string StartModule = "start";
        public const string Open = "open";
        public const string Search = "search";
        public const string Close = "close";
        public const string Shutup = "shutup";
        public const string Show = "gotofront";
        public const string Hide = "gotobackground";
        public const string Exit = "exit";
        public const string Gom = "gom";

        public static readonly string[] AllCommands =
        {
            "start encryptor",
            "close encryptor",
            "start securedpass",
            "start movementdetection",
            "close movementdetection",

            "open google",
            "open youtube",
            "nexttab",
            "previoustab",

            "close tab",
            "close gom",

            "tell joke",
            "shutup",

            "gotobackground",
            "gotofront",
            "stop",
            "exit",

            "gom pause",
            "gom back",
            "gom next"
        };

        public const string SecureDesktopPath =
            "..\\..\\..\\Jarvis.SecureDesktop\\bin\\Debug\\Jarvis.SecureDesktop.exe";
        public const string EncryptorPath =
            "..\\..\\..\\Jarvis.Encryptor\\bin\\Debug\\Jarvis.Encryptor.exe";
        public const string MovementDetectionPath =
            "..\\..\\..\\Jarvis.MovementDetection\\bin\\Debug\\Jarvis.MovementDetection.exe";

        public const string SecureDesktopFile = "Jarvis.SecureDesktop";
        public const string EncryptorFile = "Jarvis.Encryptor";
        public const string MovementDetectionFile = "Jarvis.MovementDetection";
    }
}
