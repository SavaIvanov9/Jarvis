using System;
namespace Jarvis.Organizer.CommandReceiving
{
    using System.Collections.Generic;
    using System.Threading;
    using Receivers;

    public class ReceiverManager : IReceiverManager
    {
        private readonly List<IReceiver> _receivers;

        public ReceiverManager()
        {
            this._receivers = new List<IReceiver>();
        }

        public void AddReceiver(IReceiver receiver)
        {
            _receivers.Add(receiver);
        }

        public void StartReceivers()
        {
            for (int i = 0; i < _receivers.Count; i++)
            {
                var thread = new Thread(_receivers[i].Start);
                thread.Start();
            }
        }

        public void StopReceivers()
        {
            for (int i = 0; i < _receivers.Count; i++)
            {
                _receivers[i].Stop();
            }
        }
    }
}
