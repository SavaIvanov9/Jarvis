namespace Jarvis.Commons.Logger
{
    public interface ILogger
    {
        void LogCommand(string message);

        void Log(string message);
    }
}
