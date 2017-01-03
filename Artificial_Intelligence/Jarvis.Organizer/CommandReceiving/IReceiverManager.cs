
namespace Jarvis.Organizer.CommandReceiving
{
    using Receivers;

    public interface IReceiverManager
    {
        void AddReceiver(IReceiver receiver);

        void StartReceivers();

        void StopReceivers();
    }
}
