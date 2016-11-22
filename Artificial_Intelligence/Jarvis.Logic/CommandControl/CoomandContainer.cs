using System.Collections.Generic;

namespace Jarvis.Logic.CommandControl
{
    public delegate void OnMenuClickHandler(string selectedValue);

    public class CoomandContainer
    {
        public event OnMenuClickHandler OnMenuClick;

        private void OnClick(string value)
        {
            if (OnMenuClick != null)
            {
                OnMenuClick(value);
            }
        }

        public IList<string> CommandList = new List<string>();

        public void AddCommand(string command)
        {
            CommandList.Add(command);
            OnClick(command);
        }
    }
}
