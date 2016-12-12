using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jarvis.Logic.Interaction.Interfaces;

namespace Jarvis.Logic.ProcessCommunication
{
    public sealed class KeySender
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lp1, string lp2);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]

        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private static readonly Lazy<KeySender> Lazy =
            new Lazy<KeySender>(() => new KeySender());
        
        public static KeySender Instance
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return Lazy.Value;
            }
        }

        public void Send(string windowName, string[] commands, IInteractorManager manager)
        {
            // find window handle
            IntPtr handle = FindWindow(null, windowName);
            if (!handle.Equals(IntPtr.Zero))
            {
                // activate window
                if (SetForegroundWindow(handle))
                {
                    foreach (var command in commands)
                    {
                        SendKeys.SendWait(command);
                    }
                }
            }
            else
            {
                manager.SendOutput("Program not found.");
            }
        }
    }
}
