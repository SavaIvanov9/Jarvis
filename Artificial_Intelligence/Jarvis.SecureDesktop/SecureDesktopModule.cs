namespace Jarvis.SecureDesktop
{
    using System;
    using System.Windows.Forms;
    using Interfaces;
    using Providers;
    using Providers.ClipBoardProvider;

    public sealed class SecureDesktopModule
    {
        private readonly IPasswordReceiver _passwordReceiver = new PasswordReceiver();
        private readonly IClipboardProvider _clipboardProvider = new ClipboardProvider();

        private static readonly Lazy<SecureDesktopModule> Lazy =
            new Lazy<SecureDesktopModule>(() => new SecureDesktopModule());

        private SecureDesktopModule()
        {
        }

        public static SecureDesktopModule Instance => Lazy.Value;

        public void Start()
        {
            var password = _passwordReceiver.ReceivePassword();

            _clipboardProvider.SetTextToClipboard(DataFormats.Text, password);
            Console.WriteLine("Password saved to clipboard.");
            Console.ReadKey();
        }
    }
}
