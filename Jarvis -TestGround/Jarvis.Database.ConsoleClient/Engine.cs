using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Database.Abstraction;
using Jarvis.TestDatabase;

namespace Jarvis.Database.ConsoleClient
{
    public class Engine
    {
        //public void Start(Type db)
        //{
        //    var d = (IJarvisData)Activator.CreateInstance(db);
        //    Console.WriteLine(d.Events.All().ToList().Count);
        //    //using (var d = (JarvisTestData)Activator.CreateInstance(db))
        //    //{

        //    //}
        //}

        public void Start(IJarvisData db)
        {
            Console.WriteLine(db.Events.All().ToList().Count);
        }
    }
}
