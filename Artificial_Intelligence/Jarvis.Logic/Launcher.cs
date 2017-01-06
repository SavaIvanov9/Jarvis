namespace Jarvis.Logic
{
    using System.Collections.Generic;
    using Commons.Logger;
    using Commons.CrashReporter;
    using Core;
    using Interaction.Interactors;
    using Interaction.Interfaces;
    
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
