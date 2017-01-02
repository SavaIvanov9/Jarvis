namespace Jarvis.SecureDesktop.Providers.ClipBoardProvider
{
    using Interfaces;
    using System.Windows.Forms;

    public class ClipboardProvider : StaHelper, IClipboardProvider
    {
        private string _format;
        private object _data;

        public void SetTextToClipboard(string format, object data)
        {
            _format = format;
            _data = data;

            Start();
        }

        protected override void Work()
        {
            var obj = new DataObject(
                _format,
                _data
            );

            Clipboard.SetDataObject(obj, true);
        }
    }
}
