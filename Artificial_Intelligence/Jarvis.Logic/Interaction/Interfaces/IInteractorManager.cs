using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Logic.Interaction.Interfaces
{
    public interface IInteractorManager : IOutputSendable
    {
        List<IInteractor> Interactors { get; }

        void StartInteractors();
    }
}
