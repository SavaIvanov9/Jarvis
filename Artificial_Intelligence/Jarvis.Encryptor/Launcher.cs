namespace Jarvis.Encryptor
{
    using System;

    class Launcher
    {
        static void Main()
        {
            EncryptorModule.Instance.Start(Console.Out, Console.In);
        }
    }
}
