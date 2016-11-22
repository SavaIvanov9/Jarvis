namespace Jarvis.Logic.Interaction.Interfaces
{
    public interface IInteractor : IInputParseable, IOutputSendable
    {
        void Start();
    }
}
