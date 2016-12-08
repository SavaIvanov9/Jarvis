using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jarvis.Logic.ProcessCommunication
{
    public delegate void OnExit();

    public class ComunicationManager
    {
        private static readonly Lazy<ComunicationManager> Lazy =
            new Lazy<ComunicationManager>(() => new ComunicationManager());

        public event OnExit ExitServer;
        private List<Tuple<string, Thread>> _servers;

        private ComunicationManager()
        {
            this._servers = new List<Tuple<string, Thread>>();
        }

        public static ComunicationManager Instance
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return Lazy.Value;
            }
        }

        private void OnExit()
        {
            if (ExitServer != null)
            {
                ExitServer();
            }
        }

        public int AddServer(string servername, string connectionPass)
        {
            this._servers.Add(
                new Tuple<string, Thread>(
                    servername, 
                    new Thread(() =>
                    {
                        var server = new CommunicationServer(servername, connectionPass, OnExit);
                        server.Start();
                    })));

            return _servers.Count - 1;
        }

        public void StartServer(int index)
        {
            _servers[index].Item2.Start();
        }

        public void StopServer(int index)
        {
            OnExit();
            //_servers[index].Item2.Abort();
            //_servers[index].Item2.Join();
        }
    }
}
