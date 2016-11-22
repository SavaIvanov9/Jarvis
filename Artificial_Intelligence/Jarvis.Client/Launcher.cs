﻿using Jarvis.Logic.Interaction;

namespace Jarvis.Client
{
    using Logic.Core;
    using Logic.Core.Providers.Decisions;

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
