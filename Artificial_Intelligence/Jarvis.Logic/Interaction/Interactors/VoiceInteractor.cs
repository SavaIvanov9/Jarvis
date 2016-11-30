using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using Jarvis.Commons.Logger;
using Jarvis.Logic.CommandControl;
using Jarvis.Logic.Interaction.Interfaces;

namespace Jarvis.Logic.Interaction.Interactors
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
        
        public string RecieveInput()
        {
            return _currentInput;
        }

        public void SendOutput(string output, bool isAsync)
        {
            try
            {
                //_speechSynth.SelectVoice("Microsoft David Desktop");
                //_logger.Log("Voice set to Microsoft David Desktop");
            }
            catch 
            {
                _logger.Log("Voice set to default.");
            }

            if (isAsync)
            {
                _speechSynth.SpeakAsyncCancelAll();
                _promptBuilder.ClearContent();
                _promptBuilder.AppendText(output);
                _speechSynth.SpeakAsync(_promptBuilder);
            }
            else
            {
                _promptBuilder.ClearContent();
                _promptBuilder.AppendText(output);
                _speechSynth.Speak(_promptBuilder);
            }

        }

        public void Stop()
        {
            _speechSynth.SpeakAsyncCancelAll();
        }

        public void Start()
        {
            GrammarBuilder findServices = new GrammarBuilder("Jarvis");
            findServices.Append(new Choices(CommandConstants.AllCommands));
            Grammar servicesGrammar = new Grammar(findServices);

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
            
            var input = e.Result.Text.Substring(7, e.Result.Text.Length - 7);
            //SendOutput("Command " + input);

            ////----------Uncoment for speaking comad log--------------------
            //try
            //{
            //    _speechSynth.SelectVoice("Microsoft David Desktop");
            //}
            //catch
            //{
            //}
            //_speechSynth.SpeakAsyncCancelAll();
            //_promptBuilder.ClearContent();
            //_promptBuilder.AppendText("Command " + input + " identified");
            //_speechSynth.Speak(_promptBuilder);
            ////------------------------------------------------------------
            
            for (int c = 0; c < CommandConstants.AllCommands.Length; c++)
            {
                if (input == CommandConstants.AllCommands[c])
                {
                    CommandContainer.Instance.AddCommand(_logger, input);
                }
            }
        }
    }
}
