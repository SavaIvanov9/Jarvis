using System;
using System.Threading;

namespace Jarvis.MovementDetection
{
    class Launcher
    {
        static void Main()
        {
            var movementDetector = new MovementDetector();
            var voiceController = new VoiceController();

            Console.WriteLine("Enter Password:");
            var password = Console.ReadLine();
            Console.WriteLine("Enter seconds to delay start:");
            string seconds = Console.ReadLine();
            int delayTime = 0;

            if (string.IsNullOrEmpty(seconds))
            {
                do
                {
                    Console.WriteLine("Enter valid seconds:");
                    seconds = Console.ReadLine();
                } while (!int.TryParse(seconds, out delayTime));
                
            }

            delayTime = int.Parse(seconds);
            Console.WriteLine($"Movement detection will start after {seconds} seconds.");
            voiceController.Speak($"Movement detection will start after {seconds} seconds.");
            Thread.Sleep(delayTime * 1000);
            Console.WriteLine("Movement detection started.");
            voiceController.Speak("Movement detection started.");
            Console.Clear();

            var thread = new Thread(movementDetector.VideoSource.Start);
            thread.IsBackground = true;
            thread.Start();
            
            var thread2 = new Thread(voiceController.StartVoiceAlarm);
            thread2.IsBackground = true;
            thread2.Start();

            var input = Console.ReadLine();

            while (Config.IsAlive)
            {
                if (input == password)
                {
                    movementDetector.VideoSource.SignalToStop();
                    Config.IsAlive = false;
                    break;
                }
                input = Console.ReadLine();
            }

            Console.WriteLine("Movement detection stopped");
            voiceController.Speak("Movement detection stopped");
            Console.WriteLine("Alarm activation log:");
            for (int i = 0; i < voiceController.AlarmLog.Count; i++)
            {
                Console.WriteLine($"{i + 1} >> {voiceController.AlarmLog[i]}");
            }
            Console.ReadKey();
        }
    }
}
