using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace monitoring
{
    class Program
    {
        //creating a object to be used for speaking purpose
        private static SpeechSynthesizer speaking = new SpeechSynthesizer();
        static void Main(string[] args)
        {
            speaking.Speak("Hello Welcome to the performance monitor version one point zero");
            PerformanceCounter perfcpucount = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
            PerformanceCounter perfmemorycount = new PerformanceCounter("Memory", "Available MBytes");
            while (true)
            {
                int cpuUsage = (int)perfcpucount.NextValue();
                int memoryUsage = (int)perfmemorycount.NextValue();
                if (cpuUsage > 80)
                {

                    if (cpuUsage == 100)
                    {
                        string cpuMessage = string.Format("The Current cpu usage is very high please slow down!");
                        speaks(cpuMessage, VoiceGender.Female);
                    }
                    else
                    {
                        string cpuMessage = string.Format("The Current cpu usage is {0} percent", perfcpucount.NextValue());
                        speaks(cpuMessage, VoiceGender.Male);
                    }

                }
                if (memoryUsage < 1024)
                {

                    string memoryMessage = string.Format("The Current memory avaliable is {0} megabytes ", perfmemorycount.NextValue());
                    speaks(memoryMessage, VoiceGender.Male);

                }

                Thread.Sleep(3000);
            }
        }
        static void speaks(string message, VoiceGender gen)
        {
            speaking.SelectVoiceByHints(gen);
            speaking.Speak(message);
        }
        static void speaks(string message, VoiceGender gen, int rate)
        {
            speaking.Rate = rate;
            speaking.SelectVoiceByHints(gen);
            speaking.Speak(message);
        }
    }
}
