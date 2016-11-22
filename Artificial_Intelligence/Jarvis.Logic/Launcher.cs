namespace Jarvis.Logic
{
    using Core;
    using Core.Providers.Decisions;
    using Jarvis.Logic.Interaction;

    class Launcher
    {
        static void Main()
        {
            JarvisEngine.Instance(
                new ConsoleInteractor(),
                new DecisionTaker())
                .Start();
        }
    }
}
