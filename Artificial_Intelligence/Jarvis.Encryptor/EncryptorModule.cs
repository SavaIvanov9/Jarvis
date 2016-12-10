using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using Jarvis.Encryptor.ProcessCommunication;

namespace Jarvis.Encryptor
{
    using System;
    using System.IO;
    using Commands;

    public sealed class EncryptorModule
    {
        private static Lazy<EncryptorModule> Lazy =
            new Lazy<EncryptorModule>(() => new EncryptorModule());

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
            try
            {
                writer.WriteLine("Encryptor started.");
                CommandProcessor.Instance(writer, reader).ProcessCommand("help");

                CommunicationClient receiver = new CommunicationClient(writer);
                writer.WriteLine("Started listening for connection to server for external commands...\n");
                new Thread(receiver.Start).Start();

                writer.WriteLine("Enter command:");
                //var command = reader.ReadLine();

                new Thread((() =>
                {
                    while (true)
                    {
                        var command = reader.ReadLine();
                        CommandContainer.Instance.AddCommand(command, writer);
                    }
                })).Start();
                //while (command != "close encryptor")
                //{
                //    try
                //    {
                //        //CommandProcessor.Instance(writer, reader).ProcessCommand(command);
                //        CommandContainer.Instance.AddCommand(command, writer);
                //    }
                //    catch (Exception ex)
                //    {
                //        writer.WriteLine(ex.ToString());
                //    }

                //    writer.WriteLine("Enter command:");
                //    command = reader.ReadLine();
                //}

                Application.Run();

                //writer.WriteLine("Encryptor stoped.");
            }
            catch (Exception ex)
            {
                writer.WriteLine(ex);
            }
        }
    }
}
