using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class SimpleSpeach
    {
        private static readonly SpeechSynthesizer synth = new SpeechSynthesizer();
        private static readonly PromptBuilder pBuilder = new PromptBuilder();

        static void Main(string[] args)
        {
            string command = Console.ReadLine();

            while (command != "end")
            {
                Speak(command);
                command = Console.ReadLine();
            }
        }

        private static void Speak(string Message)
        {
            synth.SelectVoice("Microsoft David Desktop");
            pBuilder.ClearContent();
            pBuilder.AppendText(Message);
            //Console.WriteLine(Message);
            synth.Speak(pBuilder);
        }
    }
}
