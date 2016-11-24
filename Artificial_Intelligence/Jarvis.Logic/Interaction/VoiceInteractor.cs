using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using Jarvis.Commons.Logger;
using Jarvis.Logic.CommandControl;
using Jarvis.Logic.Interaction.Interfaces;

namespace Jarvis.Logic.Interaction
{
    public class VoiceInteractor : IInteractor
    {
        private readonly SpeechSynthesizer _speechSynth = new SpeechSynthesizer();
        private readonly PromptBuilder _promptBuilder = new PromptBuilder();
        private readonly SpeechRecognitionEngine _speechRecognition = new SpeechRecognitionEngine();
        private readonly ILogger _logger;
        private string _currentInput = "";


        public VoiceInteractor(ILogger logger)
        {
            if (logger == null)
            {
                throw new InvalidEnumArgumentException($"Logger cannot be 0!");
            }

            this._logger = logger;
        }

        private readonly List<string> _sList = new List<string>()
        {
            "stop",
            "shutup",
            "start encryptor",
            "close encryptor",
            "start securedpass",
            "tell joke",
            "exit"
            //"exit",
            //"how are you",
            //"go to internet",
            //"jarvis i want to play some league",
            //"whats your favorite movie",
            //"play me some music",
            //"stop the music",
            //"close"
        };

        public string RecieveInput()
        {
            return _currentInput;
        }

        public Tuple<IList<string>, IList<string>> ParseInput(string inputLine)
        {
            IList<string> commandSegments = inputLine
                .Split(new[] { ": " }, StringSplitOptions.None)
                .ToList();

            IList<string> commandParts = commandSegments[0]
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            if (commandSegments.Count > 1)
            {
                IList<string> commandParams = commandSegments[1]
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

                return new Tuple<IList<string>, IList<string>>(commandParts, commandParams);
            }

            return new Tuple<IList<string>, IList<string>>(commandParts, new List<string>());
        }

        public void SendOutput(string output)
        {
            _speechSynth.SpeakAsyncCancelAll();
            _promptBuilder.ClearContent();
            _promptBuilder.AppendText(output);
            _speechSynth.SpeakAsync(_promptBuilder);
        }

        public void Stop()
        {
            _speechSynth.SpeakAsyncCancelAll();
        }

        public void Start()
        {
            GrammarBuilder findServices = new GrammarBuilder("Jarvis");
            findServices.Append(new Choices(_sList.ToArray()));

            // Create a Grammar object from the GrammarBuilder and load it to the recognizer.
            Grammar servicesGrammar = new Grammar(findServices);
            //Grammar Gram = new Grammar(servicesGrammar);
            //Grammar Gram = new Grammar(new GrammarBuilder(new Choices(_sList.ToArray())));
            try
            {
                _speechRecognition.RequestRecognizerUpdate();
                _speechRecognition.LoadGrammar(servicesGrammar);
                _speechRecognition.SpeechRecognized += SetCurrentInput;
                _speechRecognition.SetInputToDefaultAudioDevice();
                _speechRecognition.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch
            {
                return;
            }
        }

        public void StopListening()
        {
            _speechRecognition.RecognizeAsyncStop();
        }

        private void SetCurrentInput(object sender, SpeechRecognizedEventArgs e)
        {
            //_currentInput = e.Result.Text;
            //var input = _currentInput;
            //var input = e.Result.Text;
            var input = e.Result.Text.Substring(7, e.Result.Text.Length - 7);
            SendOutput("Ccommand " + input);
            //SendOutput("Command " + input + " identified");
            for (int c = 0; c < _sList.Count; c++)
            {
                if (input == _sList[c])
                {
                    CommandContainer.Instance.AddCommand(_logger, input);
                }
            }
        }
    }
}
