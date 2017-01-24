using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Database.Abstraction;

namespace Jarvis.Database.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new Engine();
            //engine.Start(typeof(JarvisDbEntities));
            //engine.Start(typeof(JarvisTestDbEntities));
            engine.Start(new JarvisData());
        }
    }
}
