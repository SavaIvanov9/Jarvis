using Jarvis.Logic.CommandControl;

namespace Jarvis.Logic.Interaction.Interfaces
{
    public interface IInputReceiveble
    {
        string RecieveInput(CoomandContainer coomandContainer);
        //Task<string> RecieveInput();
    }
}
