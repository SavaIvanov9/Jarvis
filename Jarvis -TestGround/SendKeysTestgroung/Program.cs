using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendKeysTestgroung
{
    class Program
    {
        //[DllImport("user32.dll")]
        //private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        ////[DllImport("user32.dll")]
        ////private static extern bool SetForegroundWindow(IntPtr hWnd);

        //[DllImport("User32.dll")]
        //static extern int SetForegroundWindow(IntPtr point);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lp1, string lp2);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        static void Main(string[] args)
        {
            //IntPtr zero = IntPtr.Zero;
            //for (int i = 0; (i < 60) && (zero == IntPtr.Zero); i++)
            //{
            //    Thread.Sleep(100);
            //    zero = FindWindow(null, "Jarvis.Encryptor");
            //}
            //if (zero != IntPtr.Zero)
            //{
            //    SetForegroundWindow(zero);
            //    SendKeys.SendWait("hui 4evryst");
            //    SendKeys.SendWait("{ENTER}");
            //    SendKeys.Flush();
            //}


            button1_Click();
            //Sendkey();
        }

        private static void button1_Click()
        {
            //string name = "Jarvis Encryptor";
            string name = "gomplayer";
            // find window handle of Notepad
            IntPtr handle = FindWindow(null, name);
            if (!handle.Equals(IntPtr.Zero))
            {
                // activate window
                if (SetForegroundWindow(handle))
                {
                    Console.WriteLine("hi");
                    // send "Hello World!"
                    //SendKeys.SendWait("help");
                    //SendKeys.SendWait("{ENTER}");
                    SendKeys.SendWait(" ");
                }
            }
            else
            {
                Console.WriteLine("Process not found.");
            }
        }

        static void Sendkey()
        {
            Process p = Process.GetProcessesByName("Jarvis.Encryptor").FirstOrDefault();
            if (p != null)
            {
                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);
                Thread.Sleep(5000);
                SendKeys.SendWait("k");
            }

            //Process p = Process.GetProcessesByName("Jarvis.Encryptor")[0];
            //Console.WriteLine(p.ProcessName);
            //IntPtr pointer = p.Handle;
            //SetForegroundWindow(pointer);
            //SendKeys.SendWait("k");
        }
    }
}
