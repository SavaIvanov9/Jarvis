namespace Jarvis.TestDatabase.Repositories
{
    using Abstraction;
    using Data.Models;

    public class EventsRepository : GenericRepository<Event>, IRepository<Event>
    {
        public EventsRepository(IJarvisTestDbContext context)
            : base(context)
        {
        }
    }
}