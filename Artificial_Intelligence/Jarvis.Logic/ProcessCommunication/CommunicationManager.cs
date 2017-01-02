namespace Jarvis.Logic.ProcessCommunication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using Commons.Exceptions;
    using Commons.Logger;
    using Interaction.Interfaces;

    public delegate void OnExit();

    public class CommunicationManager
    {
        private static readonly Lazy<CommunicationManager> Lazy =
            new Lazy<CommunicationManager>(() => new CommunicationManager());

        public event OnExit ExitServer;
        private readonly HashSet<Thread> _servers;

        private CommunicationManager()
        {
            this._servers = new HashSet<Thread>();
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
            if (_servers.Where(x => x.Name == servername).ToList().Count > 0)
            {
                throw new DuplicateInstanceException($"{servername} has alredy started");
            }

            var thread = new Thread(() =>
            {
                var server = new CommunicationServer(servername, connectionPass, logger, manager);
                server.Start();
            });
            thread.Name = servername;

            _servers.Add(thread);
            
            return _servers.Count - 1;
        }

        public void StartServer(int index)
        {
            _servers.ElementAt(index).Start();
        }

        public bool StopServer(string servername)
        {
            try
            {
                OnExit();
                //_servers.ElementAt(index).Join();
                _servers.RemoveWhere(x => x.Name == servername);
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
