namespace Jarvis.Logic.Interaction.Interfaces
{
    public interface IInteractor : IOutputSendable
    {
        void Start();

        void Pause();

        void Stop();
    }
}
