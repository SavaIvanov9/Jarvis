namespace Jarvis.Commons.CrashReporter
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Text;

    public class CrashReporter : IReporter
    {
        private const string ReportsFolderName = "CrashReports";

        public string CreateReport(Exception report)
        {
            CheckDirectory();

            string currentLog = $"{DateTime.Now:yyyy-MM-dd_hh;mm;ss}.txt";
            string path = Path.Combine(
                Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location),
                    $@"{ReportsFolderName}\{currentLog}");

            CreateLogFile(path, report);

            return "Report Created";
        }

        private void CheckDirectory()
        {
            string dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string pathString = Path.Combine(dirName, ReportsFolderName);
            if (!Directory.Exists(pathString))
            {
                Directory.CreateDirectory(pathString);
            }
        }

        private void CreateLogFile(string path, Exception report)
        {
            if (!File.Exists(path))
            {
                using (StreamWriter file = File.CreateText(path))
                {
                    file.Write(CreateContent(report));
                }
            }
        }

        private StringBuilder CreateContent(Exception message)
        {
            StringBuilder report = new StringBuilder();
            
            report.AppendLine("Title               : " + message.Message);
            report.AppendLine("Date                : " + DateTime.Now);
            report.AppendLine("Machine Name        : " + Environment.MachineName);
            report.AppendLine("UserDomain Name     : " + Environment.UserDomainName);
            report.AppendLine("Login User Name     : " + Environment.UserName);
            report.AppendLine("OS Version          : " + Environment.OSVersion);
            report.AppendLine("Version             : " + Environment.Version);
            report.AppendLine("Assembly Name       : " + Assembly.GetEntryAssembly().GetName());
            report.AppendLine("Assembly Location   : " + Assembly.GetEntryAssembly().Location);
            report.AppendLine(Environment.NewLine + 
                "-------------------<Call Stack>-------------------");
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();

            for (int i = 3; i < stackFrames.Length; i++)
            {
                report.AppendLine(stackFrames[i].GetMethod().ToString());
            }

            report.AppendLine(Environment.NewLine +
                "-----------------<Report Content>-----------------");
            report.AppendLine(Environment.NewLine + message);
            
            return report;
        }
    }
}
