namespace Jarvis.Logic.Interaction.Interfaces
{
    using System.Collections.Generic;

    public interface IInteractorManager : IOutputSendable
    {
        List<IInteractor> Interactors { get; }

        void Initialize(IList<IInteractor> interactors);

        void StartInteractors();

        void StopInteractors();
    }
}
