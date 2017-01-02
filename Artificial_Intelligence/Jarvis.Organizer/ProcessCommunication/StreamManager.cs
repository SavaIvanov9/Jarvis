using System;
using System.IO;
using System.Text;
//using Jarvis.Encryptor.Commands;

namespace Jarvis.Organizer.ProcessCommunication
{
    public class StreamManager
    {
        private string _command = "";
        private TextWriter _writer;
        private Stream _ioStream;
        private UnicodeEncoding _streamEncoding;

        public StreamManager(Stream ioStream, TextWriter writer)
        {
            this._writer = writer;
            this._ioStream = ioStream;
            _streamEncoding = new UnicodeEncoding();
        }

        public string Command
        {
            get { return _command; }
            set { _command = value; }
        }

        public void StartListeningForNewCommand()
        {
            //new Thread(() =>
            //{

            while (_command != "exit" || _command != "stop connection to server")
            {
                this._command = ReadString();
                //CommandContainer.Instance.AddCommand(_command, _writer);
                //_writer.WriteLine(_command);
            }
            //}).Start();
        }

        public string ReadString()
        {
            var len = _ioStream.ReadByte() * 256;
            len += _ioStream.ReadByte();
            byte[] inBuffer = new byte[len];
            _ioStream.Read(inBuffer, 0, len);

            return _streamEncoding.GetString(inBuffer);
        }

        public int WriteString(string outString)
        {
            byte[] outBuffer = _streamEncoding.GetBytes(outString);
            int len = outBuffer.Length;
            if (len > UInt16.MaxValue)
            {
                len = (int)UInt16.MaxValue;
            }
            _ioStream.WriteByte((byte)(len / 256));
            _ioStream.WriteByte((byte)(len & 255));
            _ioStream.Write(outBuffer, 0, len);
            _ioStream.Flush();

            return outBuffer.Length + 2;
        }
    }
}
