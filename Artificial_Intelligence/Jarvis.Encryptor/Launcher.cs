﻿using System;

namespace Jarvis.Encryptor
{
    class Launcher
    {
        static void Main()
        {
            EncryptorModule.Instance.Start(Console.Out, Console.In);
        }
    }
}