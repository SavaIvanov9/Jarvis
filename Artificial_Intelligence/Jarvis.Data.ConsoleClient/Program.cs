using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Data.Models;

namespace Jarvis.Data.ConsoleClient
{
    public class Program
    {
        static void Main()
        {
            var db = new JarvisData();
            var c = db.Events.All().ToList().Count();
            SaveElements();
            //RestoreElements();

            //Console.WriteLine(db.GetReadyTimes.All().Where(a => a.Date != String.Empty).ToList().Count);
            //db.GetReadyTimes.Add(new GetReadyTime
            //{
            //    Date = "dnes",
            //    StartTime = "sega",
            //    DurationInMinutes = 10
            //});

            //db.SaveChanges();
            //Console.WriteLine(db.GetReadyTimes.All().Where(a => a.Date != String.Empty).ToList().Count);
        }

        private static void SaveElements()
        {
            var db = new JarvisData();
            var sleepTimes = db.SleepTimes.All().ToList();
            using (Stream stream = File.Open("sleeptimes.txt", FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, sleepTimes);
            }

            var getReadyTimes = db.GetReadyTimes.All().ToList();
            using (Stream stream = File.Open("getreadytimes.txt", FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, getReadyTimes);
            }

            var jokes = db.Jokes.All().ToList();
            using (Stream stream = File.Open("jokes.txt", FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, jokes);
            }

            var events = db.Events.All().ToList();
            using (Stream stream = File.Open("events.txt", FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, events);
            }

            Console.WriteLine("Database serialized.");
        }

        private static void RestoreElements()
        {
            var db = new JarvisData();

            using (Stream stream = File.Open("sleeptimes.txt", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();

                var sleeptimes = (List<SleepTime>)bin.Deserialize(stream);
                foreach (SleepTime s in sleeptimes)
                {
                    db.SleepTimes.Add(s);
                }
            }
            db.SaveChanges();

            using (Stream stream = File.Open("getreadytimes.txt", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();

                var getReadyTimes = (List<GetReadyTime>)bin.Deserialize(stream);
                foreach (var s in getReadyTimes)
                {
                    db.GetReadyTimes.Add(s);
                }
            }
            db.SaveChanges();

            using (Stream stream = File.Open("jokes.txt", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();

                var jokes = (List<Joke>)bin.Deserialize(stream);
                foreach (var j in jokes)
                {
                    db.Jokes.Add(j);
                }
            }
            db.SaveChanges();

            using (Stream stream = File.Open("events.txt", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();

                var events = (List<Event>)bin.Deserialize(stream);
                foreach (var e in events)
                {
                    db.Events.Add(e);
                }
            }
            db.SaveChanges();

            Console.WriteLine("Database restored.");
        }

        //private void CheckDirectory()
        //{
        //    string dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        //    string pathString = Path.Combine(dirName, ReportsFolderName);
        //    if (!Directory.Exists(pathString))
        //    {
        //        Directory.CreateDirectory(pathString);
        //    }
        //}

        //private void CreateLogFile(string path, Exception report)
        //{
        //    if (!File.Exists(path))
        //    {
        //        using (StreamWriter file = File.CreateText(path))
        //        {
        //            file.Write(CreateContent(report));
        //        }
        //    }
        //}
    }
}
