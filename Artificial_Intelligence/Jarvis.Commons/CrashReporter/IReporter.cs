using System;

namespace Jarvis.Commons.CrashReporter
{
    public interface IReporter
    {
        string CreateReport(Exception report);
    }
}
