using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Database.Abstraction.Repsitories;

namespace Jarvis.Database.Abstraction
{
    public interface IJarvisData
    {
        EventsRepository Events
        {
            get;
        }
    }
}
