using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Commons.Logger
{
    public interface ILogger
    {
        void LogCommand(string message);

        void Log(string message);
    }
}
