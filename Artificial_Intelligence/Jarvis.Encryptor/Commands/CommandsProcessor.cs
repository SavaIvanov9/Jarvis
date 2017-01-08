using System.CodeDom.Compiler;

namespace Jarvis.Encryptor.Commands
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class CommandProcessor
    {
        private readonly TextWriter _writer;
        private readonly TextReader _reader;
        private static readonly object ConsoleWriterLock = new object();

        private CommandProcessor(TextWriter writer, TextReader reader)
        {
            CommandContainer.Instance.OnNewCommand += ProcessCommand;
            this._writer = writer;
            this._reader = reader;
        }

        public static CommandProcessor Instance(TextWriter writer, TextReader reader)
        {
            return new CommandProcessor(writer, reader);
        }

        public void ProcessCommand(string command)
        {
            lock (ConsoleWriterLock)
            {
                switch (command)
                {
                    case EncryptorConstants.Help:
                        this.DisplayCommands();
                        break;
                    case EncryptorConstants.EncryptString:
                        this.EncryptString();
                        break;
                    case EncryptorConstants.DecryptStryng:
                        this.DecryptString();
                        break;
                    case EncryptorConstants.EncryptTxtFile:
                        this.EncryptTxtFile();
                        break;
                    case EncryptorConstants.DecryptTxtFile:
                        this.DecryptTxtFile();
                        break;
                    case EncryptorConstants.ClearConsole:
                        this.ClearConsole();
                        break;
                    case EncryptorConstants.Exit:
                        this.Exit();
                        break;
                    default:
                        _writer.WriteLine(@"Unknown command. Type ""help"" for a list of commands.");
                        break;
                }
            }
        }

        private void EncryptString()
        {
            _writer.WriteLine("Enter a password to use:");
            string password = _reader.ReadLine();
            _writer.WriteLine("Enter a string to encrypt:");
            string text = _reader.ReadLine();
            _writer.WriteLine(Environment.NewLine);

            _writer.WriteLine("Your encrypted string is:");
            string encryptedstring = Cipher.Encrypt(text, password);
            _writer.WriteLine(encryptedstring);
            _writer.WriteLine("");
        }

        private void DecryptString()
        {
            _writer.WriteLine("Enter a password to use:");
            string password = _reader.ReadLine();
            _writer.WriteLine("Enter a string to decrypt:");
            string text = _reader.ReadLine();
            _writer.WriteLine("");

            _writer.WriteLine("Your decrypted string is:");
            string decryptedstring = Cipher.Decrypt(text, password);
            _writer.WriteLine(decryptedstring);
            _writer.WriteLine("");
        }

        private void EncryptTxtFile()
        {
            _writer.WriteLine("Enter file path:");
            string path = _reader.ReadLine();
            _writer.WriteLine("Enter password:");
            string password = _reader.ReadLine();

            List<List<string>> text = new List<List<string>>();

            using (StreamReader reader = new StreamReader(path))
            {
                string line = reader.ReadLine();

                while (line != null)
                {
                    if (line == "")
                    {
                        text.Add(new List<string> { "" });
                    }
                    else
                    {
                        text.Add(new List<string>(line.Split(' ')).Select(
                            t => Cipher.Encrypt(t, password)).ToList());
                    }

                    line = reader.ReadLine();
                }
            }

            CheckDirectory("EncryptedFiles");
            DirectoryInfo dir = new DirectoryInfo(".");
            String dirName = dir.FullName;
            string newFilePath = Path.Combine(dirName, "EncryptedFiles");
            string newFileName = "";

            if (path != null && path.Contains(@"\"))
            {
                newFileName = path.Substring(path.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
            }
            else
            {
                newFileName = path;
            }

            if (!CheckFile(newFilePath, newFileName))
            {
                using (StreamWriter file = File.CreateText(Path.Combine(newFilePath, newFileName)))
                {
                    for (int i = 0; i < text.Count; i++)
                    {
                        file.WriteLine(string.Join(" ", text[i]));
                    }

                    _writer.WriteLine($"File {newFileName} encrypted in folder EncryptedFiles.");
                }
            }
            else
            {
                _writer.WriteLine("File alredy exists.");
            }

            //_writer.WriteLine("Enter file path:");
            //string path = _reader.ReadLine();
            //_writer.WriteLine("Enter password:");
            //string password = _reader.ReadLine();

            //List<string> text = new List<string>();

            //using (StreamReader reader = new StreamReader(path))
            //{
            //    string line = reader.ReadLine();

            //    while (line != null)
            //    {
            //        text.Add(StringCipher.Encrypt(line, password));

            //        line = reader.ReadLine();
            //    }
            //}

            //CheckDirectory("EncryptedFiles");
            //DirectoryInfo dir = new DirectoryInfo(".");
            //String dirName = dir.FullName;
            //string newFilePath = Path.Combine(dirName, "EncryptedFiles");
            //string newFileName = "";

            //if (path != null && path.Contains(@"\"))
            //{
            //    newFileName = path.Substring(path.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
            //}
            //else
            //{
            //    newFileName = path;
            //}

            //if (!CheckFile(newFilePath, newFileName))
            //{
            //    using (StreamWriter file = File.CreateText(Path.Combine(newFilePath, newFileName)))
            //    {
            //        for (int i = 0; i < text.Count; i++)
            //        {
            //            file.WriteLine(string.Join(" ", text[i]));
            //        }

            //        _writer.WriteLine($"File {newFileName} encrypted in folder EncryptedFiles.");
            //    }
            //}
            //else
            //{
            //    _writer.WriteLine("File alredy exists.");
            //}
        }

        private void DecryptTxtFile()
        {
            _writer.WriteLine("Enter file path:");
            string path = _reader.ReadLine();
            _writer.WriteLine("Enter password:");
            string password = _reader.ReadLine();

            List<List<string>> text = new List<List<string>>();

            using (StreamReader reader = new StreamReader(path))
            {
                string line = reader.ReadLine();

                while (line != null)
                {
                    if (line == "")
                    {
                        text.Add(new List<string> { "" });
                    }
                    else
                    {
                        text.Add(new List<string>(line.Split(' ')).Select(
                        t => Cipher.Decrypt(t, password)).ToList());
                    }

                    line = reader.ReadLine();
                }
            }

            CheckDirectory("DecryptedFiles");
            DirectoryInfo dir = new DirectoryInfo(".");
            String dirName = dir.FullName;
            string newFilePath = Path.Combine(dirName, "DecryptedFiles");
            string newFileName = "";

            if (path != null && path.Contains(@"\"))
            {
                newFileName = path.Substring(path.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
            }
            else
            {
                newFileName = path;
            }

            if (!CheckFile(newFilePath, newFileName))
            {
                using (StreamWriter file = File.CreateText(Path.Combine(newFilePath, newFileName)))
                {
                    for (int i = 0; i < text.Count; i++)
                    {
                        file.WriteLine(string.Join(" ", text[i]));
                    }

                    _writer.WriteLine($"File {newFileName} decrypted in folder DecryptedFiles.");
                }
            }
            else
            {
                _writer.WriteLine("File alredy exists.");
            }
            //_writer.WriteLine("Enter file path:");
            //string path = _reader.ReadLine();
            //_writer.WriteLine("Enter password:");
            //string password = _reader.ReadLine();

            //List<string> text = new List<string>();

            //using (StreamReader reader = new StreamReader(path))
            //{
            //    string line = reader.ReadLine();

            //    while (line != null)
            //    {
            //        text.Add(StringCipher.Decrypt(line, password));

            //        line = reader.ReadLine();
            //    }
            //}

            //CheckDirectory("DecryptedFiles");
            //DirectoryInfo dir = new DirectoryInfo(".");
            //String dirName = dir.FullName;
            //string newFilePath = Path.Combine(dirName, "DecryptedFiles");
            //string newFileName = "";

            //if (path != null && path.Contains(@"\"))
            //{
            //    newFileName = path.Substring(path.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
            //}
            //else
            //{
            //    newFileName = path;
            //}

            //if (!CheckFile(newFilePath, newFileName))
            //{
            //    using (StreamWriter file = File.CreateText(Path.Combine(newFilePath, newFileName)))
            //    {
            //        for (int i = 0; i < text.Count; i++)
            //        {
            //            file.WriteLine(string.Join(" ", text[i]));
            //        }

            //        _writer.WriteLine($"File {newFileName} decrypted in folder DecryptedFiles.");
            //    }
            //}
            //else
            //{
            //    _writer.WriteLine("File alredy exists.");
            //}
        }

        private void DisplayCommands()
        {
            StringBuilder commandsDescription = new StringBuilder();

            commandsDescription.AppendLine("------------------------------------------" + Environment.NewLine +
                                           "COMMANDS: " + Environment.NewLine +
                                           "encrypt string - Encrypts input string" + Environment.NewLine +
                                           "decrypt string - Decrypts input string" + Environment.NewLine +
                                           "encrypt txt file - Encrypts whole txt file" + Environment.NewLine +
                                           "decrypt txt file - Decrypts whole txt file" + Environment.NewLine +
                                           "clear - Clears console" + Environment.NewLine +
                                           "------------------------------------------");
            _writer.Write(commandsDescription.ToString());
        }

        private void ClearConsole()
        {
            Console.Clear();
        }

        private void CheckDirectory(string name)
        {
            DirectoryInfo dir = new DirectoryInfo(".");
            String dirName = dir.FullName;
            string pathString = Path.Combine(dirName, name);

            if (!Directory.Exists(pathString))
            {
                Directory.CreateDirectory(pathString);
            }
        }

        private bool CheckFile(string directory, string file)
        {
            string[] filePaths = Directory.GetFiles(directory, "*.txt");
            string[] fileNames = filePaths.Select(x => x.Substring(x.LastIndexOf(@"\", StringComparison.Ordinal) + 1)).ToArray();

            return fileNames.Contains(file);
        }

        private void Exit()
        {
            _writer.WriteLine("Encryptor stoped.");
            Environment.Exit(0);
        }
    }
}
