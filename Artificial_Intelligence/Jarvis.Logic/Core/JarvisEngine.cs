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
        private readonly InteractorManager _interactorManager;
        private readonly ILogger _logger;

        private JarvisEngine(InteractorManager manager, ILogger logger)
        {
            this._interactorManager = manager;
            this._logger = logger;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static JarvisEngine Instance(InteractorManager manager, ILogger logger)
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
            CommandProcessor.Instance.Start(_interactorManager);

            LiveMyEvilCreation();
        }

        private void LiveMyEvilCreation()
        {
            try
            {
                //_interactorManager.Interactors[1].Start();
                StartInteractors(_interactorManager);

                StayAlive();
            }
            catch (Exception ex)
            {
                _interactorManager.SendOutput(ex.ToString());
                //_interactorManager.Interactors[0].SendOutput(ex.ToString());
            }
            finally
            {
                LiveMyEvilCreation();
            }
        }

        private void StartInteractors(InteractorManager interactorManager)
        {
            for (int i = 0; i < interactorManager.Interactors.Count; i++)
            {
                new Thread(interactorManager.Interactors[i].Start).Start();
            }
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
