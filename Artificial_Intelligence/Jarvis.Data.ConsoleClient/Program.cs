using System;
using System.Collections.Generic;
using System.Linq;
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
            Console.WriteLine(db.GetReadyTimes.All().Where(a => a.Date != String.Empty).ToList().Count);
            db.GetReadyTimes.Add(new GetReadyTime
            {
                Date = "dnes",
                StartTime = "sega",
                DurationInMinutes = 10
            });
            db.SaveChanges();
            Console.WriteLine(db.GetReadyTimes.All().Where(a => a.Date != String.Empty).ToList().Count);
        }
    }
}
