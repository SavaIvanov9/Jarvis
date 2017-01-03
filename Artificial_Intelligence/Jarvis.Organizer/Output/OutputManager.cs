using System.Speech.Synthesis;
using Jarvis.Commons.Logger;

namespace Jarvis.Organizer.Output
{
    public class OutputManager : IOutputManager
    {
        private readonly ILogger _logger;
        private readonly SpeechSynthesizer _synth = new SpeechSynthesizer();
        private readonly PromptBuilder _pBuilder = new PromptBuilder();

        public OutputManager(ILogger logger)
        {
            this._logger = logger;
        }

        public void SendOutput(string output)
        {
            _logger.Log(output);
            Speak(output);
        }

        private void Speak(string message)
        {
            _synth.SelectVoice("Microsoft David Desktop");
            _pBuilder.ClearContent();
            _pBuilder.AppendText(message);
            _synth.Speak(_pBuilder);
        }
    }
}
