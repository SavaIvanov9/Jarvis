using System;
using System.Collections;
using System.Collections.Generic;

namespace Jarvis.Logic.CommandControl
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

        public static readonly string[] AllCommands =
        {
            "nexttab",
            "previoustab",
            "close tab",
            "gotobackground",
            "gotofront",
            "stop",
            "shutup",
            "start encryptor",
            "close encryptor",
            "start securedpass",
            "open google",
            "open youtube",
            "tell joke",
            "exit"
        };
    }
}
