using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using Jarvis.Commons.Logger;
using Jarvis.Encryptor.Commands;
using Jarvis.Logic.CommandControl.Constants;
using Jarvis.Logic.Interaction.Interfaces;
using CommandContainer = Jarvis.Logic.CommandControl.CommandContainer;

namespace Jarvis.Logic.Interaction.Interactors
{
    public class VoiceInteractor : IInteractor
    {
        private readonly SpeechSynthesizer _speaker = new SpeechSynthesizer();
        private readonly PromptBuilder _promptBuilder = new PromptBuilder();
        private readonly SpeechRecognitionEngine _listener =
            new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
        private readonly ILogger _logger;

        //private readonly ILogger _logger;
        //private readonly PromptBuilder _promptBuilder = new PromptBuilder();
        //private SpeechSynthesizer _speaker;
        //private SpeechRecognitionEngine _listener;
        
        public VoiceInteractor(ILogger logger)
        {
            if (logger == null)
            {
                throw new InvalidEnumArgumentException($"Logger cannot be 0!");
            }

            this._logger = logger;
        }
        
        public void SendOutput(string output, bool isAsync)
        {
            //using (_speaker = new SpeechSynthesizer())
            //{
                try
                {
                    //_speaker.SelectVoice("Microsoft David Desktop");
                    //_logger.Log("Voice set to Microsoft David Desktop");
                }
                catch
                {
                    _logger.Log("Voice set to default.");
                }

                if (isAsync)
                {
                    _speaker.SpeakAsyncCancelAll();
                    _promptBuilder.ClearContent();
                    _promptBuilder.AppendText(output);
                    _speaker.SpeakAsync(_promptBuilder);
                }
                else
                {
                    _promptBuilder.ClearContent();
                    _promptBuilder.AppendText(output);
                    _speaker.Speak(_promptBuilder);
                }
            //}
        }

        public void Pause()
        {
            _speaker.SpeakAsyncCancelAll();
        }

        public void Stop()
        {
            _listener.RecognizeAsyncStop();
            _listener.UnloadAllGrammars();
            _listener.Dispose();
            _speaker.Dispose();
        }

        public void Start()
        {
            //using (_listener = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US")))
            //{
                GrammarBuilder findServices = new GrammarBuilder("Jarvis");
                findServices.Append(new Choices(CommandConstants.AllCommands));
                //findServices..Append(new Choices(EncryptorConstants.ValidChoises));
                Grammar servicesGrammar = new Grammar(findServices);

                try
                {
                    _listener.RequestRecognizerUpdate();
                    _listener.LoadGrammar(servicesGrammar);
                    _listener.SpeechRecognized += SetCurrentInput;
                    _listener.SetInputToDefaultAudioDevice();
                    _listener.RecognizeAsync(RecognizeMode.Multiple);
                }
                catch
                {
                    return;
                }
            //}
        }
        
        private void SetCurrentInput(object sender, SpeechRecognizedEventArgs e)
        {
            
            var input = e.Result.Text.Substring(7, e.Result.Text.Length - 7);
            //SendOutput("Command " + input);


            ////----------Uncoment for speaking command log--------------------
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
            ////---------------------------------------------------------------
            

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
