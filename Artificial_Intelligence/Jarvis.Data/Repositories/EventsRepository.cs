namespace Jarvis.Data.Repositories
{
    using Models;
    using Abstraction;

    public class EventsRepository : GenericRepository<Event>, IRepository<Event>
    {
        public EventsRepository(IJarvisDbContext context)
            : base(context)
        {
        }
    }
}
