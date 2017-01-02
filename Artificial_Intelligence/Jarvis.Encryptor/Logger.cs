namespace Jarvis.Encryptor
{
    using System;
    using System.Runtime.CompilerServices;

    public sealed class Logger
    {
        private static readonly Lazy<Logger> Lazy =
            new Lazy<Logger>(() => new Logger());
        
        public static Logger Instance
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return Lazy.Value;
            }
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void Write(string message)
        {
            Console.Write(message);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
