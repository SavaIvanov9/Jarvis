using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using Jarvis.Logic.CommandControl;
using Jarvis.Logic.Interaction.Interfaces;

namespace Jarvis.Logic.Interaction
{
    public class VoiceController
    {
        private readonly IInteractor _interactor;
        private SpeechSynthesizer Synth = new SpeechSynthesizer();
        private PromptBuilder PBuilder = new PromptBuilder();
        private SpeechRecognitionEngine Engine = new SpeechRecognitionEngine();
        private string currentInput = "";

        private List<string> sList = new List<string>()
        {
            "run encryptor",
            "stop encryptor",
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

        public VoiceController(IInteractor interactor)
        {
            this._interactor = interactor;
        }

        public string RecieveInput()
        {
            return currentInput;
        }

        public Tuple<IList<string>, IList<string>> ParseInput(string inputLine)
        {
            throw new NotImplementedException();
        }

        public void SendOutput(string output)
        {
            throw new NotImplementedException();
        }

        public void Speak(string Message)
        {
            PBuilder.ClearContent();
            PBuilder.AppendText(Message);
            Synth.Speak(PBuilder);
        }

        public void StartListening()
        {
            Grammar Gram = new Grammar(new GrammarBuilder(new Choices(sList.ToArray())));
            try
            {
                Engine.RequestRecognizerUpdate();
                Engine.LoadGrammar(Gram);
                Engine.SpeechRecognized += SetCurrentInput;
                Engine.SetInputToDefaultAudioDevice();
                Engine.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch
            {
                return;
            }
        }

        public void StopListening()
        {
            Engine.RecognizeAsyncStop();
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

            currentInput = e.Result.Text;
            //JarvisEngine.Instance(new ConsoleInteractor(), new DecisionTaker()).commandLine = e.Result.Text;
            //Console.WriteLine(e.Result.Text.ToString());
            Speak(e.Result.Text);
            
            var shits = _interactor.ParseInput(currentInput);
            //CommandProcessor.Instance.ProcessCommand(shits.Item1, shits.Item2, _interactor);

            for (int c = 0; c < sList.Count; c++)
            {
                if (currentInput == sList[c])
                {
                    CommandContainer.Instance.AddCommand(currentInput);
                    
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
