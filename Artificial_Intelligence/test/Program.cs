using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Data;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new JarvisData();
            var getRedyTimes = db.GetReadyTimes.All().Where(a => a.Date == "").ToList();
            Console.WriteLine(getRedyTimes.Count);
        }
    }
}
