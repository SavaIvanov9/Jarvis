using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading;
using System.Diagnostics;

namespace Jarvis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpeechSynthesizer Synth = new SpeechSynthesizer();
        PromptBuilder PBuilder = new PromptBuilder();
        SpeechRecognitionEngine Engine = new SpeechRecognitionEngine();
        MediaPlayer MPlayer = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            JarvisSpeak(TextBox1.Text);
        }

        private void JarvisSpeak(string Message)
        {
            PBuilder.ClearContent();
            PBuilder.AppendText(Message);
            Synth.Speak(PBuilder);
            JarvisToConsole(Message);
        }

        private void JarvisToConsole(string Message)
        {
            TextBlock.Text += "\n >Jarvis : " + Message;
        }

        private void WriteToConsole(string Message)
        {
            TextBlock.Text += "\n >" + Message;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button2.IsEnabled = false;
            Button3.IsEnabled = true;
            Choices sList = new Choices();
            sList.Add(new string[] {
                "hello",
                "exit",
                "how are you",
                "go to internet",
                "jarvis i want to play some league",
                "whats your favorite movie",
                "play me some music",
                "stop the music",
                "close" });

            Grammar Gram = new Grammar(new GrammarBuilder(sList));
            try
            {
                Engine.RequestRecognizerUpdate();
                Engine.LoadGrammar(Gram);
                Engine.SpeechRecognized += Engine_SpeechRecognized;
                Engine.SetInputToDefaultAudioDevice();
                Engine.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch
            {
                
                return;
            }
        }

        private void Engine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            WriteToConsole(e.Result.Text.ToString());
            switch (e.Result.Text.ToString())
            {
                case "exit":
                    JarvisSpeak("Shutting down!");
                    Application.Current.Shutdown();
                    break;
                case "close":
                    JarvisSpeak("Goodbye sir.");
                    Application.Current.Shutdown();
                    break;
                case "hello":
                    JarvisSpeak("Good evening sir.");
                    break;
                case "how are you":
                    JarvisSpeak("Just fine sir.");
                    break;
                case "go to internet":
                    JarvisSpeak("Yes sir.");
                    Process.Start("http://www.google.bg");
                    break;
                case "jarvis i want to play some league":
                    JarvisSpeak("Ofcourse sir.");
                    Process.Start("F:/GAIMZ/LoL/lol.launcher.exe");
                    break;
                case "whats your favorite movie":
                    JarvisSpeak("But ofcourse its, Iron Man");
                    break;
                case "play me some music":
                    JarvisSpeak("Yes sir.");
                    MPlayer.Open(new Uri(@"../../Sounds/IRsound.mp3", UriKind.Relative));
                    MPlayer.Play();
                    break;
                case "stop the music":
                    MPlayer.Stop();
                    JarvisSpeak("Anything else sir?");
                    break;
                default:
                    break;
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            Engine.RecognizeAsyncStop();
            Button2.IsEnabled = true;
            Button3.IsEnabled = false;
        }
    }
}
