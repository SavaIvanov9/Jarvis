namespace Jarvis.Client
{
    using System.Collections.Generic;
    using Commons.Logger;
    using Commons.CrashReporter;
    using Logic.Core;
    using Logic.Interaction.Interactors;
    using Logic.Interaction.Interfaces;
    
    public class Launcher
    {
        public static void Main()
        {
            var logger = new ConsoleLogger();
            var interactors = new List<IInteractor>()
            {
                new ConsoleInteractor(logger),
                new VoiceInteractor(logger)
            };
            var reporter = new CrashReporter();

            JarvisEngine.Instance(logger, interactors, reporter).Start();
        }
    }
}
