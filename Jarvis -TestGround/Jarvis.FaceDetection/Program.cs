using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarvis.FaceDetection.Recognition;

namespace Jarvis.FaceDetection
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new Engine();
            engine.Start();
        }
    }
}
