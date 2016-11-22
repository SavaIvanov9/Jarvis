namespace Jarvis.Logic
{
    using Core;
    using Interaction;
    using Commons.Logger;

    class Launcher
    {
        static void Main()
        {
            var logger = new ConsoleLogger();
            var manager = new InteractorManager();
            manager.AddInteractor(new ConsoleInteractor(logger));
            manager.AddInteractor(new VoiceInteractor(logger));
            
            JarvisEngine.Instance(manager, logger)
                .Start();
        }
    }
}
