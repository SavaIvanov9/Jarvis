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
        private readonly SpeechSynthesizer _synth = new SpeechSynthesizer();
        private readonly PromptBuilder _pBuilder = new PromptBuilder();
        private readonly SpeechRecognitionEngine _engine = new SpeechRecognitionEngine();
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
            "run encryptor",
            "stop encryptor",
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
            _pBuilder.ClearContent();
            _pBuilder.AppendText(output);
            _synth.Speak(_pBuilder);
        }

        public void Start()
        {
            Grammar Gram = new Grammar(new GrammarBuilder(new Choices(_sList.ToArray())));
            try
            {
                _engine.RequestRecognizerUpdate();
                _engine.LoadGrammar(Gram);
                _engine.SpeechRecognized += SetCurrentInput;
                _engine.SetInputToDefaultAudioDevice();
                _engine.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch
            {
                return;
            }
        }

        public void StopListening()
        {
            _engine.RecognizeAsyncStop();
        }

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        private void SetCurrentInput(object sender, SpeechRecognizedEventArgs e)
        {

            //Process p = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName)[0];
            ////Console.WriteLine(p);
            //IntPtr pointer = p.Handle;
            //SetForegroundWindow(pointer);

            //Process p = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).FirstOrDefault();
            //if (p != null)
            //{
            //    IntPtr h = p.MainWindowHandle;
            //    SetForegroundWindow(h);
            //}

            _currentInput = e.Result.Text;
            //JarvisEngine.Instance(new ConsoleInteractor(), new DecisionTaker()).commandLine = e.Result.Text;
            //Console.WriteLine(e.Result.Text.ToString());
            SendOutput(e.Result.Text);
            
            var shits = ParseInput(_currentInput);
            //CommandProcessor.Instance.ProcessCommand(shits.Item1, shits.Item2, _interactor);

            for (int c = 0; c < _sList.Count; c++)
            {
                if (_currentInput == _sList[c])
                {
                    CommandContainer.Instance.AddCommand(_logger, _currentInput);
                    
                    //CommandProcessor.Instance.ProcessCommand(shits.Item1, shits.Item2, _interactor);
                    //SendKeys.SendWait(currentInput);
                    //SendKeys.SendWait(Environment.NewLine);
                }
            }

            //switch (e.Result.Text.ToString())
            //{
            //    case "exit":
            //        currentInput = "exit";
            //        break;
            //    //case "close":
            //    //    JarvisSpeak("Goodbye sir.");
            //    //    //Application.Current.Shutdown();
            //    //    break;
            //    //case "hello":
            //    //    JarvisSpeak("Good evening sir.");
            //    //    break;
            //    //case "how are you":
            //    //    JarvisSpeak("Just fine sir.");
            //    //    break;
            //    //case "go to internet":
            //    //    JarvisSpeak("Yes sir.");
            //    //    Process.Start("http://www.google.bg");
            //    //    break;
            //    //case "jarvis i want to play some league":
            //    //    JarvisSpeak("Ofcourse sir.");
            //    //    //Process.Start("F:/Games/LoL/lol.launcher.exe");
            //    //    break;
            //    //case "whats your favorite movie":
            //    //    JarvisSpeak("But ofcourse its, Iron Man");
            //    //    break;
            //    //case "play me some music":
            //    //    JarvisSpeak("Yes sir.");
            //    //    //MPlayer.Open(new Uri(@"../../Sounds/IRsound.mp3", UriKind.Relative));
            //    //    //MPlayer.Play();
            //    //    break;
            //    //case "stop the music":
            //    //    //MPlayer.Stop();
            //    //    JarvisSpeak("Anything else sir?");
            //    //    break;
            //    default:
            //        break;
            //}
        }
    }
}
