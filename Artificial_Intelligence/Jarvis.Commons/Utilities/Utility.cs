using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Jarvis.Commons.Utilities
{
    public sealed class Utility
    {
        private readonly char[] _alphabet;

        private readonly Random _random = new Random();

        private static readonly Lazy<Utility> Lazy =
            new Lazy<Utility>(() => new Utility());

        private Utility()
        {
            _alphabet =
                Enumerable.Range('A', 26).Select(c => (char)c).Concat(
                Enumerable.Range('a', 26).Select(c => (char)c).Concat(
                Enumerable.Range('0', 10).Select(c => (char)c)))
                .ToArray();

        }


        public static Utility Instance
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get { return Lazy.Value; }
        }

        public int RandomNumber(int min = 0, int max = int.MaxValue / 2)
        {
            if (min > max)
            {
                return _random.Next(max, min + 1);
            }

            return _random.Next(min, max + 1);
        }

        public string RandomString(int minLength = 0, int maxLength = int.MaxValue / 2)
        {
            var length = RandomNumber(minLength, maxLength);
            var result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(_alphabet[_random.Next(0, _alphabet.Length)]);
            }

            return result.ToString();
        }

        public DateTime RandomDateTime(DateTime after, DateTime before)
        {
            int range = (before - after).Days;
            return after.AddDays(_random.Next(range));

            //var afterValue = after ?? new DateTime(2000, 1, 1, 0, 0, 0);
            //var beforeValue = before ?? DateTime.Now.AddDays(-60);

            //var second = RandomNumber(afterValue.Second, beforeValue.Second);
            //var minute = RandomNumber(afterValue.Minute, beforeValue.Minute);
            //var hour = RandomNumber(afterValue.Hour, beforeValue.Hour);
            //var day = RandomNumber(afterValue.Day, beforeValue.Day);
            //var month = RandomNumber(afterValue.Month, beforeValue.Month);
            //var year = RandomNumber(afterValue.Year, beforeValue.Year);

            //if (day > 28)
            //{
            //    day = 28;
            //}

            //return new DateTime(year, month, day, hour, minute, second);
        }

        //[DllImport("kernel32.dll", ExactSpelling = true)]
        //public static extern IntPtr GetConsoleWindow();

        //[DllImport("user32.dll")]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //public static extern bool SetForegroundWindow(IntPtr hWnd);

        //public void BringConsoleToFront()
        //{
        //    SetForegroundWindow(GetConsoleWindow());
        //}


        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        public void Show()
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_SHOW);
        }

        public void Hide()
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
        }


        private const int SW_MAXIMIZE = 3;
        private const int SW_MINIMIZE = 6;
        // more here: http://www.pinvoke.net/default.aspx/user32.showwindow

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);

        public void Show2()
        {
            IntPtr hwnd = FindWindowByCaption(IntPtr.Zero, "The window title");
            ShowWindow(hwnd, SW_MAXIMIZE);
        }

        enum ShowWindowCommands
        {
            /// <summary>
            /// Hides the window and activates another window.
            /// </summary>
            Hide = 0,
            /// <summary>
            /// Activates and displays a window. If the window is minimized or
            /// maximized, the system restores it to its original size and position.
            /// An application should specify this flag when displaying the window
            /// for the first time.
            /// </summary>
            Normal = 1,
            /// <summary>
            /// Activates the window and displays it as a minimized window.
            /// </summary>
            ShowMinimized = 2,
            /// <summary>
            /// Maximizes the specified window.
            /// </summary>
            Maximize = 3, // is this the right value?
                          /// <summary>
                          /// Activates the window and displays it as a maximized window.
                          /// </summary>      
            ShowMaximized = 3,
            /// <summary>
            /// Displays a window in its most recent size and position. This value
            /// is similar to <see cref="Win32.ShowWindowCommand.Normal"/>, except
            /// the window is not activated.
            /// </summary>
            ShowNoActivate = 4,
            /// <summary>
            /// Activates the window and displays it in its current size and position.
            /// </summary>
            Show = 5,
            /// <summary>
            /// Minimizes the specified window and activates the next top-level
            /// window in the Z order.
            /// </summary>
            Minimize = 6,
            /// <summary>
            /// Displays the window as a minimized window. This value is similar to
            /// <see cref="Win32.ShowWindowCommand.ShowMinimized"/>, except the
            /// window is not activated.
            /// </summary>
            ShowMinNoActive = 7,
            /// <summary>
            /// Displays the window in its current size and position. This value is
            /// similar to <see cref="Win32.ShowWindowCommand.Show"/>, except the
            /// window is not activated.
            /// </summary>
            ShowNA = 8,
            /// <summary>
            /// Activates and displays the window. If the window is minimized or
            /// maximized, the system restores it to its original size and position.
            /// An application should specify this flag when restoring a minimized window.
            /// </summary>
            Restore = 9,
            /// <summary>
            /// Sets the show state based on the SW_* value specified in the
            /// STARTUPINFO structure passed to the CreateProcess function by the
            /// program that started the application.
            /// </summary>
            ShowDefault = 10,
            /// <summary>
            ///  <b>Windows 2000/XP:</b> Minimizes a window, even if the thread
            /// that owns the window is not responding. This flag should only be
            /// used when minimizing windows from a different thread.
            /// </summary>
            ForceMinimize = 11
        }
    }
}