using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Commons.Logger;
using Jarvis.Data;
using Jarvis.Data.Models;
using Jarvis.Organizer.CommandReceiving;
using Jarvis.Organizer.Output;

namespace Jarvis.Organizer.CommandControl
{
    public sealed class CommandProcessor
    {
        private static readonly Lazy<CommandProcessor> Lazy =
            new Lazy<CommandProcessor>(() => new CommandProcessor());
        private readonly IJarvisData _jarvisData = new JarvisData();

        private const string CommandNotFoundMsg = "Command not found.";
        private const string InvalidParametersMsg = "Invalid Parameters.";

        public static CommandProcessor Instance
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get { return Lazy.Value; }
        }

        public void StartSleepRecording(IOutputManager outputManager, ILogger logger)
        {
            var newSleepTime = new SleepTime
            {
                Date = DateTime.Now.ToShortDateString(),
                StartTime = DateTime.Now.ToString("t"),
                IsEnded = false
            };

            _jarvisData.SleepTimes.Add(newSleepTime);
            _jarvisData.SaveChanges();

            outputManager.SendOutput("New sleeptime added to database.");
            logger.Log($"Start date: {newSleepTime.Date}");
            logger.Log($"Start time: {newSleepTime.StartTime}");
            logger.Log($"IsEnded: {newSleepTime.IsEnded}");
        }

        public void StoptSleepRecording(IOutputManager outputManager, ILogger logger)
        {
            var sleepTime = _jarvisData
                .SleepTimes
                .All()
                .ToList()[_jarvisData.SleepTimes.All().Count() - 1];

            if (sleepTime.IsEnded)
            {
                outputManager.SendOutput("There is no new data.");
            }
            else
            {
                var startDateParts = sleepTime.Date.Split(
                    new[] { "." }, StringSplitOptions.None);
                //Console.WriteLine(startDateParts[0]);
                //Console.WriteLine(startDateParts[1]);
                //Console.WriteLine(startDateParts[2].Substring(0, 4));

                var startTimeParts = sleepTime.StartTime.Split(
                    new[] { ":" }, StringSplitOptions.None);
                //Console.WriteLine(startTimeParts[0]);
                //Console.WriteLine(startTimeParts[1]);

                var duration = DateTime.Now.Subtract(
                    new DateTime(
                        int.Parse(startDateParts[2].Substring(0, 4)),
                        int.Parse(startDateParts[1]),
                        int.Parse(startDateParts[0]),
                        int.Parse(startTimeParts[0]),
                        int.Parse(startTimeParts[1]),
                        0));

                //Console.WriteLine(duration.ToString().Substring(0, duration.ToString().Length-8));

                sleepTime.IsEnded = true;
                sleepTime.Duration = duration.ToString().Substring(0, duration.ToString().Length - 8);

                outputManager.SendOutput($"Last sleep duration is {sleepTime.Duration}");
                _jarvisData.SleepTimes.All().ToList()[_jarvisData.SleepTimes.All().Count() - 1] = sleepTime;
                _jarvisData.SaveChanges();
            }
        }

        public void GetSleepStatistic(IOutputManager outputManager, ILogger logger)
        {
            var dateParts = DateTime.Now.ToShortDateString()
                .Split(new[] { "." }, StringSplitOptions.None);

            var data = _jarvisData
                .SleepTimes
                .All()
                .ToList()
                .Where(x => x.IsEnded
                && x.Date.Split(new[] { "." }, StringSplitOptions.None)[2] == dateParts[2]
                && x.Date.Split(new[] { "." }, StringSplitOptions.None)[1] == dateParts[1]
                && int.Parse(x.Date.Split(new[] { "." }, StringSplitOptions.None)[0]) >= int.Parse(dateParts[0]) - 7);


            var firstDate = data.ToList().OrderBy(x => x.Date).ToList()[0];
            //foreach (var d in dateParts)
            //{
            //    Console.WriteLine(d);
            //}
            var days = (new DateTime(
                int.Parse(dateParts[2].Substring(0, 4)),
                int.Parse(dateParts[1]),
                int.Parse(dateParts[0])) - new DateTime(
                    int.Parse(firstDate.Date.Split(new[] { "." }, StringSplitOptions.None)[2].Substring(0, 4)),
                    int.Parse(firstDate.Date.Split(new[] { "." }, StringSplitOptions.None)[1]),
                    int.Parse(firstDate.Date.Split(new[] { "." }, StringSplitOptions.None)[0]))).TotalDays + 1;

            //var totalDuration = data.Sum(x => long.Parse(x.Duration));
            TimeSpan totalDuration = new TimeSpan();
            foreach (var sleepTime in data)
            {
                totalDuration += (TimeSpan.Parse(sleepTime.Duration));
            }

            outputManager.SendOutput($"Total sleep for last {days} days is {totalDuration}");

            var sleepPerDay = new TimeSpan((long)(totalDuration.Ticks / days));

            outputManager.SendOutput($"Average sleep per day is {sleepPerDay.Hours} hours {sleepPerDay.Minutes} minutes");
        }

        public void AddEvent(IOutputManager outputManager, ILogger logger)
        {
            logger.Log(@"Enter date in format ""dd.mm.yyyy"":");
            var dateValues = Console.ReadLine().Trim().Split('.');
            var date = new DateTime(
                int.Parse(dateValues[2]),
                int.Parse(dateValues[1]),
                int.Parse(dateValues[0]))
                .ToShortDateString();

            logger.Log(@"Enter start time in format ""hh:mm"":");
            var time = Console.ReadLine();

            logger.Log("Enter title:");
            var title = Console.ReadLine();

            _jarvisData.Events.Add(new Event
            {
                Date = date,
                StartTime = time,
                Title = title
            });

            _jarvisData.SaveChanges();
            logger.Log($@"Event ""{title}"" added to database.");
        }

        public void GetEventsData(IOutputManager outputManager, ILogger logger)
        {
            var dateToday = DateTime.Today.ToShortDateString();
            var events = _jarvisData.Events.Find(e => e.Date == dateToday).ToList();

            outputManager.SendOutput($"You have {events.Count} events for today.");
            
            foreach (var e in events)
            {
                outputManager.SendOutput($"{e.Title} starting at {e.StartTime}");
            }
        }

        public void Exit(IReceiverManager receiverManager)
        {
            receiverManager.StopReceivers();
            Environment.Exit(0);
        }
    }
}
