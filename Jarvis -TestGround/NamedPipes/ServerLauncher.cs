using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NamedPipes
{
    class ServerLauncher
    {
        static void Main(string[] args)
        {
            ExampleServer exampleServer = new ExampleServer();
            exampleServer.Start();
            //exampleServer.Start();
            //JarvisServer jarvisServer = new JarvisServer();
            //jarvisServer.Start();
        }
    }
}