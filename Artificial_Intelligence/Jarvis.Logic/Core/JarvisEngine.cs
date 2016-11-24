using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using Jarvis.Commons.Logger;
using Jarvis.Commons.Utilities;
using Jarvis.Logic.CommandControl;
using Jarvis.Logic.Core.Interfaces.Decisions;
using Jarvis.Logic.Interaction;
using Jarvis.Logic.Interaction.Interfaces;

namespace Jarvis.Logic.Core
{
    public class JarvisEngine
    {
        private readonly IInteractorManager _interactorManager;
        private readonly ILogger _logger;

        private JarvisEngine(IInteractorManager manager, ILogger logger)
        {
            this._interactorManager = manager;
            this._logger = logger;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static JarvisEngine Instance(IInteractorManager manager, ILogger logger)
        {
            if (manager.Interactors.Count == 0)
            {
                throw new InvalidEnumArgumentException($"Interactors cannot be 0!");
            }

            if (logger == null)
            {
                throw new InvalidEnumArgumentException($"Logger cannot be 0!");
            }

            return new JarvisEngine(manager, logger);
        }

        public void Start()
        {
            CommandManager.Instance.Start(_interactorManager, _logger);
            _interactorManager.StartInteractors();
            StayAlive();
        }
        
        private void StayAlive()
        {
            while (true)
            {
                _interactorManager.Interactors[0].Start();
            }
        }
    }
}
