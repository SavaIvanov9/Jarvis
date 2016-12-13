namespace Jarvis.Logic
{
    using System.Collections.Generic;
    using Commons.Logger;
    using Core;
    using Interaction.Interactors;
    using Interaction.Interfaces;

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
