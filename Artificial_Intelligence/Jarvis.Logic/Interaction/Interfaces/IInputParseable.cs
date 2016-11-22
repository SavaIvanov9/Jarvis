using System;
using System.Collections.Generic;

namespace Jarvis.Logic.Interaction.Interfaces
{
    public interface IInputParseable
    {
        Tuple<IList<string>, IList<string>> ParseInput(string inputLine);
    }
}
