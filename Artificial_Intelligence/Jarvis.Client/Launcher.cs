namespace Jarvis.Client
{
    using System.Collections.Generic;
    using Commons.Logger;
    using Logic.Core;
    using Logic.Interaction.Interactors;
    using Logic.Interaction.Interfaces;
    
    class Launcher
    {
        static void Main()
        {
            var logger = new ConsoleLogger();
            var interactors = new List<IInteractor>()
            {
                new ConsoleInteractor(logger),
                new VoiceInteractor(logger)
            };

            JarvisEngine.Instance(logger, interactors).Start();
        }
    }
}
