using System.Runtime.CompilerServices;

namespace Jarvis.Encryptor
{
    using System;
    using System.IO;
    using Commands;

    public sealed class EncryptorModule
    {
        private static  Lazy<EncryptorModule> Lazy =
            new Lazy<EncryptorModule>(() => new EncryptorModule());

        private readonly TextWriter _writer = Console.Out;
        private readonly TextReader _reader = Console.In;

        private EncryptorModule()
        {
            Console.Title = "Jarvis Encryptor";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static EncryptorModule Instance
        {
            get
            {
                return Lazy.Value;
            }
        } 

        public void Start(TextWriter writer, TextReader reader)
        {
            writer.WriteLine("Encryptor started. Enter command:");
            var command = reader.ReadLine();

            while (command != "stop encryptor")
            {
                if (!string.IsNullOrEmpty(command))
                {
                    try
                    {
                        CommandProcessor.Instance(writer, reader).ProcessCommand(command);
                    }
                    catch (FileNotFoundException)
                    {
                        writer.WriteLine("File not found.");
                    }
                    catch (Exception ex)
                    {
                        writer.WriteLine(ex.ToString());
                    }
                }
                else
                {
                    writer.WriteLine(@"Unknown command. Type ""help"" for a list of commands.");
                }

                writer.WriteLine("Enter command:");
                command = reader.ReadLine();
            }

            writer.WriteLine("Encryptor stoped.");
        }
    }
}
