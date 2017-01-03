namespace Jarvis.Organizer.CommandReceiving.Receivers
{
    using System;
    using CommandControl;

    public class ConsoleReceiver : IReceiver
    {
        private bool _isActive;

        public void Start()
        {
            _isActive = true;

            while (true)
            {
                if (_isActive == false)
                {
                    break;
                }
                var command = Console.ReadLine();
                CommandContainer.Instance.AddCommand(command);
            }
        }

        public void Stop()
        {
            _isActive = false;
        }
    }
}
