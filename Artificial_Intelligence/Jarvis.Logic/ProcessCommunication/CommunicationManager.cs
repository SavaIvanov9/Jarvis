using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Jarvis.Commons.Logger;
using Jarvis.Logic.Interaction.Interfaces;

namespace Jarvis.Logic.ProcessCommunication
{
    public delegate void OnExit();

    public class CommunicationManager
    {
        private static readonly Lazy<CommunicationManager> Lazy =
            new Lazy<CommunicationManager>(() => new CommunicationManager());

        public event OnExit ExitServer;
        private List<Thread> _servers;

        private CommunicationManager()
        {
            this._servers = new List<Thread>();
        }

        public static CommunicationManager Instance
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get { return Lazy.Value; }
        }

        private void OnExit()
        {
            if (ExitServer != null)
            {
                ExitServer();
            }
        }

        public int AddServer(string servername, string connectionPass, ILogger logger, IInteractorManager manager)
        {
            _servers.Add(
                new Thread(() =>
                {
                    var server = new CommunicationServer(servername, connectionPass, logger, manager);
                    server.Start();
                })
            );
            
            return _servers.Count - 1;
        }

        public void StartServer(int index)
        {
            _servers[index].Start();
        }

        public bool StopServer(int index)
        {
            try
            {
                OnExit();
                _servers[index].Join();
                _servers.RemoveAt(index);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void StopAllServers()
        {
            foreach (var server in _servers)
            {
                OnExit();
            }
        }
    }
}
