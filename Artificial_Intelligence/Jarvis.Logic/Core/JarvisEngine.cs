using Jarvis.Commons.CrashReporter;

namespace Jarvis.Logic.Core
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using Commons.Logger;
    using CommandControl;
    using Interaction;
    using Interaction.Interfaces;

    public class JarvisEngine
    {
        private readonly ManualResetEvent _quitEvent = new ManualResetEvent(false);
        private readonly IInteractorManager _interactorManager = new InteractorManager();
        private readonly ILogger _logger;
        private readonly IReporter _reporter;

        private JarvisEngine(ILogger logger, IList<IInteractor> interactors, IReporter reporter)
        {
            this._interactorManager.Initialize(interactors);
            this._logger = logger;
            this._reporter = reporter;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static JarvisEngine Instance(
            ILogger logger, IList<IInteractor> interactors, IReporter reporter)
        {
            if (interactors.Count == 0)
            {
                throw new ArgumentException($"Interactors cannot be 0!");
            }

            if (logger == null)
            {
                throw new ArgumentException($"Logger cannot be 0!");
            }

            if (reporter == null)
            {
                throw new ArgumentException($"Reporter cannot be 0!");
            }

            return new JarvisEngine(logger, interactors, reporter);
        }

        public void Start()
        {
            try
            {
                _interactorManager.StartInteractors();
                CommandManager.Instance.Start(_interactorManager, _logger, _reporter);
                StayAlive();
            }
            catch (Exception ex)
            {
                Console.WriteLine(_reporter.CreateReport(ex));
            }
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
