using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Database.Abstraction.Repsitories.Base;

namespace Jarvis.Database.Abstraction.Repsitories
{
    public class EventsRepository : GenericRepository<Event>, IRepository<Event>
    {
        public EventsRepository(DbContext context)
            : base(context)
        {
        }
    }
}
