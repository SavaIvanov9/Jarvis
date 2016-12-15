using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using Jarvis.Commons.Logger;
using Jarvis.Commons.Utilities;
using Jarvis.Logic.CommandControl;
using Jarvis.Logic.Interaction;
using Jarvis.Logic.Interaction.Interfaces;

namespace Jarvis.Logic.Core
{
    public class JarvisEngine
    {
        private readonly ManualResetEvent _quitEvent = new ManualResetEvent(false);
        private readonly IInteractorManager _interactorManager = new InteractorManager();
        private readonly ILogger _logger;

        private JarvisEngine(ILogger logger, IList<IInteractor> interactors)
        {
            _interactorManager.Initialize(interactors);
            this._logger = logger;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static JarvisEngine Instance(ILogger logger, IList<IInteractor> interactors)
        {
            if (interactors.Count == 0)
            {
                throw new InvalidEnumArgumentException($"Interactors cannot be 0!");
            }

            if (logger == null)
            {
                throw new InvalidEnumArgumentException($"Logger cannot be 0!");
            }

            return new JarvisEngine(logger, interactors);
        }

        public void Start()
        {
            CommandManager.Instance.Start(_interactorManager, _logger);
            _interactorManager.StartInteractors();
            StayAlive();
        }

        private void StayAlive()
        {
            Console.CancelKeyPress += (sender, eArgs) =>
            {
                _quitEvent.Set();
                eArgs.Cancel = true;
            };
            
            _quitEvent.WaitOne();

            //Alternative effect
            //Application.Run();
        }
    }
}
