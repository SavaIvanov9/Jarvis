using System;
using System.Collections.Generic;
using System.Threading;
using Jarvis.Logic.Interaction.Interfaces;

namespace Jarvis.Logic.Interaction
{
    public class InteractorManager : IInteractorManager
    {
        private readonly List<IInteractor> _interactors = new List<IInteractor>();
        private List<Thread> _activeInteractors = new List<Thread>();

        public void Initialize(IList<IInteractor> interactors)
        {
            foreach (var interactor in interactors)
            {
                _interactors.Add(interactor);
            }
        }

        public void AddInteractor(IInteractor interactor)
        {
            _interactors.Add(interactor);
        }

        public void SendOutput(string message, bool isAsync)
        {
            foreach (var interactor in _interactors)
            {
                interactor.SendOutput(message, isAsync);
            }
        }

        public List<IInteractor> Interactors
        {
            get { return this._interactors; }
        }

        public void StartInteractors()
        {
            for (int i = 0; i < _interactors.Count; i++)
            {
                var thread = new Thread(_interactors[i].Start);
                _activeInteractors.Add(thread);
                thread.Start();
                //new Thread(_interactors[i].Start).Start();
            }
        }

        public void StopInteractors()
        {
            foreach (var interactor in _interactors)
            {
                interactor.Stop();
            }

            foreach (var thread in _activeInteractors)
            {
                thread.Abort();

            }
        }
    }
}
