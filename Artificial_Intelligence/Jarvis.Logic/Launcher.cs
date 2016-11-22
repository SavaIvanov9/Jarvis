using System.Collections.Generic;
using Jarvis.Logic.Interaction.Interfaces;

namespace Jarvis.Logic
{
    using Core;
    using Core.Providers.Decisions;
    using Jarvis.Logic.Interaction;

    class Launcher
    {
        static void Main()
        {
            var manager = new InteractorManager();
            manager.AddInteractor(new ConsoleInteractor());
            manager.AddInteractor(new VoiceInteractor());
            
            manager.SendOutput("hi");

            JarvisEngine.Instance(manager)
                .Start();
        }
    }
}
