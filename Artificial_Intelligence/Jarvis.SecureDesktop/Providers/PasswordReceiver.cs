namespace Jarvis.SecureDesktop.Providers
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Enums;
    using Interfaces;

    public class PasswordReceiver : IPasswordReceiver
    {
        [DllImport("user32.dll")]
        public static extern IntPtr CreateDesktop(string lpszDesktop, IntPtr lpszDevice, IntPtr pDevmode, int dwFlags, uint dwDesiredAccess, IntPtr lpsa);

        [DllImport("user32.dll")]
        private static extern bool SwitchDesktop(IntPtr hDesktop);

        [DllImport("user32.dll")]
        public static extern bool CloseDesktop(IntPtr handle);

        [DllImport("user32.dll")]
        public static extern bool SetThreadDesktop(IntPtr hDesktop);

        [DllImport("user32.dll")]
        public static extern IntPtr GetThreadDesktop(int dwThreadId);

        [DllImport("kernel32.dll")]
        public static extern int GetCurrentThreadId();

        public string ReceivePassword()
        {
            IntPtr oldDesktop = GetThreadDesktop(GetCurrentThreadId());

            IntPtr newDesktop = CreateDesktop("Secured Desktop", IntPtr.Zero, IntPtr.Zero, 0, (uint)DesktopAccess.GenericAll, IntPtr.Zero);

            SwitchDesktop(newDesktop);

            string passwd = "";

            //running on a different thread, this way SetThreadDesktop won't fail
            Task.Factory.StartNew(() =>
            {
                //assigning the new desktop to this thread - so the Form will be shown in the new desktop
                SetThreadDesktop(newDesktop);

                Form secureWindow = new Form();
                secureWindow.Text = "Secured text";

                TextBox passwordTextBox = new TextBox();
                passwordTextBox.Location = new Point(10, 30);
                passwordTextBox.Width = 250;
                passwordTextBox.Font = new Font("Arial", 20, FontStyle.Regular);

                secureWindow.Controls.Add(passwordTextBox);
                secureWindow.FormClosing += (sender, e) =>
                {
                    passwd = passwordTextBox.Text;
                };

                Application.Run(secureWindow);

            }).Wait();

            SwitchDesktop(oldDesktop);

            CloseDesktop(newDesktop);

            return passwd;
        }
    }
}
