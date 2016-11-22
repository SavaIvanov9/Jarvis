using System.Collections.Generic;
using Jarvis.Logic.Interaction.Interfaces;

namespace Jarvis.Logic.Interaction
{
    public class InteractorManager
    {
        private List<IInteractor> _interactors = new List<IInteractor>();

        public void AddInteractor(IInteractor interactor)
        {
            _interactors.Add(interactor);
        }

        public void SendOutput(string message)
        {
            foreach (var interactor in _interactors)
            {
                interactor.SendOutput(message);
            }
        }

        public List<IInteractor> Interactors
        {
            get { return this._interactors; }
        }
    }
}
