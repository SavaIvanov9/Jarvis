﻿using System.Threading;

namespace Jarvis.Logic.Interaction.Interactors
{
    using System;
    using System.Speech.Recognition;
    using System.Speech.Synthesis;
    using Commons.Logger;
    using CommandControl.Constants;
    using Interfaces;
    using CommandControl;

    public class VoiceInteractor : IInteractor
    {
        private SpeechSynthesizer _speaker = new SpeechSynthesizer();
        private readonly PromptBuilder _promptBuilder = new PromptBuilder();
        private SpeechRecognitionEngine _listener =
            new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
        private readonly ILogger _logger;
        private bool _isActive = true;
        //private readonly ILogger _logger;
        //private readonly PromptBuilder _promptBuilder = new PromptBuilder();
        //private SpeechSynthesizer _speaker;
        //private SpeechRecognitionEngine _listener;

        public VoiceInteractor(ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentException($"Logger cannot be 0!");
            }

            this._logger = logger;
        }

        public bool IsActive
        {
            get { return _isActive; }
        }

        public void SendOutput(string output, bool isAsync)
        {
            //using (_speaker = new SpeechSynthesizer())
            //{
            try
            {
                _speaker.SelectVoice("Microsoft David Desktop");
                //_logger.LogCommand("Voice set to Microsoft David Desktop");
            }
            catch
            {
                _logger.LogCommand("Voice set to default.");
            }

            if (isAsync)
            {
                _listener.RecognizeAsyncStop();
                _speaker.SpeakAsyncCancelAll();
                _promptBuilder.ClearContent();
                _promptBuilder.AppendText(output);
                _speaker.Speak(_promptBuilder);
                //_listener.Recognize();
                _listener.RecognizeAsync(RecognizeMode.Multiple);
            }
            else
            {
                _listener.RecognizeAsyncStop();
                _promptBuilder.ClearContent();
                _promptBuilder.AppendText(output);
                _speaker.Speak(_promptBuilder);
                //_listener.Recognize();
                _listener.RecognizeAsync(RecognizeMode.Multiple);
            }
            //}
        }

        public void Pause()
        {
            _speaker.SpeakAsyncCancelAll();
        }

        public void Stop()
        {
            _isActive = false;
            _listener.RecognizeAsyncStop();
            _listener.UnloadAllGrammars();
            _listener.Dispose();
            _speaker.Dispose();
        }

        public void Start()
        {
            _isActive = true;
            _speaker = new SpeechSynthesizer();
            _listener =
            new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            //using (_listener = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US")))
            //{
            GrammarBuilder findServices = new GrammarBuilder("Jarvis");
            findServices.Append(new Choices(CommandConstants.AllCommands));
            //findServices..Append(new Choices(EncryptorConstants.ValidChoises));
            Grammar servicesGrammar = new Grammar(findServices);
            try
            {
                //_listener.
                _listener.RequestRecognizerUpdate();
                _listener.LoadGrammar(servicesGrammar);
                _listener.SpeechRecognized += SetCurrentInput;
                _listener.SetInputToDefaultAudioDevice();
                _listener.RecognizeAsync(RecognizeMode.Multiple);
                //_listener.Recognize();
            }
            catch (Exception ex)
            {
                _logger.LogCommand(ex.ToString());
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
