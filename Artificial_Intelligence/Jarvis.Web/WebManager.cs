using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Jarvis.Web
{
    public sealed class WebManager
    {
        private static readonly Lazy<WebManager> Lazy =
           new Lazy<WebManager>(() => new WebManager());

        private WebManager()
        {
        }

        public static WebManager Instance => Lazy.Value;

        public void OpenSite(IList<string> commandParams)
        {
            string site = commandParams[0];

            Process browser = new Process
            {
                StartInfo =
                        {
                            FileName = "firefox.exe",
                            Arguments = site.Trim('\0'),
                            WindowStyle = ProcessWindowStyle.Maximized
                        }
            };

            browser.Start();
        }

        public void WebSearch(IList<string> commandParams)
        {
            string qwery = string.Join("+", commandParams);
            string site = @"http://" + @"www.google.com/#hl=en&q=" + qwery;

            Process browser = new Process
            {
                StartInfo =
                {
                    FileName = "firefox.exe",
                    Arguments = site.Trim('\0'),
                    WindowStyle = ProcessWindowStyle.Maximized
                }
            };
            browser.Start();
            
        }

        public void ConnectToLocalServer(IList<string> commandParams)
        {
            
        }

        public void OpenLocalServer(IList<string> commandParams)
        {
            
        }
    }
}
