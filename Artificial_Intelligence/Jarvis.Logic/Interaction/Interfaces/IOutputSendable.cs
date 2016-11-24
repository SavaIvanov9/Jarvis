namespace Jarvis.Logic.Interaction.Interfaces
{
    public interface IOutputSendable
    {
        void SendOutput(string output, bool isAsync = true);
    }
}
