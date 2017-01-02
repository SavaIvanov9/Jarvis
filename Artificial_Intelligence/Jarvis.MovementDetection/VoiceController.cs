namespace Jarvis.MovementDetection
{
    using System;
    using System.Collections.Generic;
    using System.Speech.Synthesis;

    public class VoiceController
    {
        private SpeechSynthesizer _speaker = new SpeechSynthesizer();
        private readonly PromptBuilder _promptBuilder = new PromptBuilder();
        private readonly List<DateTime> _alarmLog = new List<DateTime>();

        public List<DateTime> AlarmLog
        {
            get { return this._alarmLog; }
        }

        public void Speak(string message)
        {
            try
            {
                //using (_speaker = new SpeechSynthesizer())
                //{
                    _speaker.SpeakAsyncCancelAll();
                    _speaker.SelectVoice("Microsoft David Desktop");
                    _promptBuilder.ClearContent();
                    _promptBuilder.AppendText(message);
                    _speaker.Speak(_promptBuilder);
                //}
            }
            catch 
            {
                
            }
        }

        public void StartVoiceAlarm()
        {
            while (Config.IsAlive)
            {
                if (Config.IsActivatedAlarm)
                {
                    //Console.WriteLine("Warrning!Intruder detected!");
                    Speak("Warrning!Intruder detected!");
                    _alarmLog.Add(DateTime.Now);
                }
            }
        }
    }
}
