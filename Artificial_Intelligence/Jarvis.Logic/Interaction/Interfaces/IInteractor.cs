namespace Jarvis.Logic.Interaction.Interfaces
{
    public interface IInteractor : IOutputSendable
    {
        bool IsActive { get; }

        void Start();

        void Pause();

        void Stop();
    }
}
