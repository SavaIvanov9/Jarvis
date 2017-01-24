using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestGround
{
    class Program
    {
        static SpeechSynthesizer Synth = new SpeechSynthesizer();
        static PromptBuilder PBuilder = new PromptBuilder();
        static SpeechRecognitionEngine Engine = new SpeechRecognitionEngine();

        static void Main(string[] args)
        {
            
            //// Create a SpeechRecognitionEngine object for the default recognizer in the en-US locale.
            //using (
            //    SpeechRecognitionEngine recognizer =
            //        new SpeechRecognitionEngine(
            //            new System.Globalization.CultureInfo("en-US")))
            //{

            //    // Create a grammar for finding services in different cities.
            //    Choices services = new Choices(new string[] { "restaurants", "hotels", "gas stations" });
            //    Choices cities = new Choices(new string[] { "Seattle", "Boston", "Dallas" });

            //    GrammarBuilder findServices = new GrammarBuilder("Find");
            //    findServices.Append(services);
            //    findServices.Append("near");
            //    findServices.Append(cities);

            //    // Create a Grammar object from the GrammarBuilder and load it to the recognizer.
            //    Grammar servicesGrammar = new Grammar(findServices);
            //    recognizer.LoadGrammarAsync(servicesGrammar);

            //    // Add a handler for the speech recognized event.
            //    recognizer.SpeechRecognized +=
            //        new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);

            //    // Configure the input to the speech recognizer.
            //    recognizer.SetInputToDefaultAudioDevice();

            //    // Start asynchronous, continuous speech recognition.
            //    recognizer.RecognizeAsync(RecognizeMode.Multiple);

            //    JarvisSpeak("Hi, i am Jarvis");

            //    // Keep the console window open.
            //    while (true)
            //    {
            //        JarvisSpeak("Say something");
            //        Console.ReadLine();
            //    }

            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //// Create a new SpeechRecognitionEngine instance.
            //SpeechRecognizer recognizer = new SpeechRecognizer();

            //// Create a simple grammar that recognizes "red", "green", or "blue".
            //Choices colors = new Choices();
            //colors.Add(new string[] { "red", "green", "blue" });

            //// Create a GrammarBuilder object and append the Choices object.
            //GrammarBuilder gb = new GrammarBuilder();
            //gb.Append(colors);

            //// Create the Grammar instance and load it into the speech recognition engine.
            //Grammar g = new Grammar(gb);
            //recognizer.LoadGrammar(g);

            //// Register a handler for the SpeechRecognized event.
            //recognizer.SpeechRecognized +=
            //  new EventHandler<SpeechRecognizedEventArgs>(Engine_SpeechRecognized);

            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //while (true)
            //{
            //    Button_Click_1();
            //  //  recognizer.SpeechRecognized +=
            //  //new EventHandler<SpeechRecognizedEventArgs>(Engine_SpeechRecognized);

            //}

            //JarvisSpeak("Hi, i am Jarvis");
            //Button_Click_1();
            //}
            Speak("Dormammu i've come to bargain.");
            //JarvisSpeak("start encryptor");
            Button_Click_1();
            // Keep the console window open.
            while (true)
            {
                //JarvisSpeak("Say something");
                //Console.ReadLine();
            }
        }


        // Handle the SpeechRecognized event.
        static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("Recognized text: " + e.Result.Text);
            Speak("Recognized text: " + e.Result.Text);
            Speak("Say something");
        }

        private static void Speak(string Message)
        {

            PBuilder.ClearContent();
            PBuilder.AppendText(Message);
            Console.WriteLine(Message);
            Synth.Speak(PBuilder);
        }

        private void JarvisToConsole(string Message)
        {
            Console.WriteLine("\n >Jarvis : " + Message);
        }

        private static void WriteToConsole(string Message)
        {
            Console.WriteLine("\n >" + Message);
        }

        private static void Button_Click_1()
        {
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
            Engine.RequestRecognizerUpdate();
            Engine.LoadGrammar(Gram);
            Engine.SpeechRecognized += Engine_SpeechRecognized;
            Engine.SetInputToDefaultAudioDevice();
            Engine.RecognizeAsync(RecognizeMode.Multiple);
            //try
            //{
            //    Engine.RequestRecognizerUpdate();
            //    Engine.LoadGrammar(Gram);
            //    Engine.SpeechRecognized += Engine_SpeechRecognized;
            //    Engine.SetInputToDefaultAudioDevice();
            //    Engine.RecognizeAsync(RecognizeMode.Multiple);
            //}
            //catch
            //{

            //    return;
            //}
        }

        private static void Engine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            WriteToConsole(e.Result.Text.ToString());
            switch (e.Result.Text.ToString())
            {
                case "exit":
                    JarvisSpeak("Shutting down!");
                    //Application.Current.Shutdown();
                    break;
                case "close":
                    JarvisSpeak("Goodbye sir.");
                    //Application.Current.Shutdown();
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
                    Process.Start("F:/Games/LoL/lol.launcher.exe");
                    break;
                case "whats your favorite movie":
                    JarvisSpeak("But ofcourse its, Iron Man");
                    break;
                case "play me some music":
                    JarvisSpeak("Yes sir.");
                    //MPlayer.Open(new Uri(@"../../Sounds/IRsound.mp3", UriKind.Relative));
                    //MPlayer.Play();
                    break;
                case "stop the music":
                    //MPlayer.Stop();
                    JarvisSpeak("Anything else sir?");
                    break;
                default:
                    break;
            }
        }
    }
}
