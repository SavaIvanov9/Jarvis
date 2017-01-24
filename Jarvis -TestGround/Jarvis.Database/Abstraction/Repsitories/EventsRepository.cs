namespace Jarvis.Database.Abstraction.Repsitories
{
    using System.Data.Entity;
    using Jarvis.Database.Abstraction.Repsitories.Base;

    public class EventsRepository : GenericRepository<Event>, IRepository<Event>
    {
        public EventsRepository(DbContext context)
            : base(context)
        {
        }
    }
}
