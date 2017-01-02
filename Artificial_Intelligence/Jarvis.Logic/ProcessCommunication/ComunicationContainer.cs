namespace Jarvis.Logic.ProcessCommunication
{
    using System;
    using System.Collections.Generic;

    public delegate void OnNewMessageHandler(string selectedValue);

    public class CommunicationContainer
    {
        public event OnNewMessageHandler OnNewMessage;

        private static readonly Lazy<CommunicationContainer> Lazy =
            new Lazy<CommunicationContainer>(() => new CommunicationContainer());
        private readonly IList<string> _commandList = new List<string>();

        public static CommunicationContainer Instance => Lazy.Value;
        
        private void OnAdd(string value)
        {
            if (OnNewMessage != null)
            {
                OnNewMessage(value);
            }
        }
        
        public void AddMessage(string message)
        {
            _commandList.Add(message);
            OnAdd(message);
        }
    }
}
