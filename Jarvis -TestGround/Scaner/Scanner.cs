using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Scaner
{
    public class Scanner
    {
        [DllImport("User32.dll")]
        public static extern int GetWindowText(int hwnd, StringBuilder s, int nMaxCount);

        [DllImport("User32.dll")]
        public static extern int GetForegroundWindow();

        public StringBuilder GetFirstValues()
        {
            StringBuilder targetComputer = new StringBuilder();

            //targetComputer.AppendLine("---------------------------------------------------------");
            //targetComputer.AppendLine("Started at         : " + DateTime.Now);
            //targetComputer.AppendLine("MachineName        : " + Environment.MachineName);
            //targetComputer.AppendLine("UserDomainName     : " + Environment.UserDomainName);
            //targetComputer.AppendLine("UserName           : " + Environment.UserName);
            //targetComputer.AppendLine("OSVersion          : " + Environment.OSVersion);
            //targetComputer.AppendLine("Version            : " + Environment.Version);
            //targetComputer.AppendLine("ActiveAppTitle     : " + ActiveAppTitle());
            //targetComputer.AppendLine("---------------------------------------------------------");

            //StringBuilder targetComputer = new StringBuilder();
            //targetComputer.AppendLine(StringCipher.Encrypt(
            //    "---------------------------------------------------------");
            //targetComputer.AppendLine(StringCipher.Encrypt(
            //    $"Started at         : {DateTime.Now}");
            //targetComputer.AppendLine(StringCipher.Encrypt(
            //    $"MachineName        : {Environment.MachineName}");
            //targetComputer.AppendLine(StringCipher.Encrypt(
            //    $"UserDomainName     : {Environment.UserDomainName}");
            //targetComputer.AppendLine(StringCipher.Encrypt(
            //    $"UserName           : {Environment.UserName}");
            //targetComputer.AppendLine(StringCipher.Encrypt(
            //    $"OSVersion          : {Environment.OSVersion}");
            //targetComputer.AppendLine(StringCipher.Encrypt(
            //    $"Version            : {Environment.Version}");
            //targetComputer.AppendLine(StringCipher.Encrypt(
            //    $"ActiveAppTitle     : {ActiveAppTitle()}");
            //targetComputer.AppendLine(StringCipher.Encrypt(
            //    "---------------------------------------------------------");

            ////Encrypted data
            targetComputer.AppendLine("---------------------------------------------------------");
            targetComputer.AppendLine($"Started at          : {(DateTime.Now)}");
            targetComputer.AppendLine($"MachineName         : {Environment.MachineName}");
            targetComputer.AppendLine($"UserDomainName      : {Environment.UserDomainName}");
            targetComputer.AppendLine($"UserName            : {Environment.UserName}");
            targetComputer.AppendLine($"OS Version          : {Environment.OSVersion}");
            targetComputer.AppendLine($"Version             : {Environment.Version}");
            targetComputer.AppendLine($"LocalIp             : {LocalIPAddress()}");
            targetComputer.AppendLine($"ActiveAppTitle      : {ActiveAppTitle()}");
            targetComputer.AppendLine("---------------------------------------------------------");

            return targetComputer;
        }

        private IPAddress LocalIPAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }

        private string ActiveAppTitle()
        {
            int hwnd = GetForegroundWindow();
            StringBuilder sbTitle = new StringBuilder(1024);

            int lenght = GetWindowText(hwnd, sbTitle, sbTitle.Capacity);

            if (lenght <= 0 || lenght > sbTitle.Length) return "unknown";
            string title = sbTitle.ToString();
            return title;
        }
    }
}
