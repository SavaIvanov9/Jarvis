namespace Jarvis.TestDatabase.Repositories
{
    using Abstraction;
    using Data.Models;

    public class SleepTimesRepository : GenericRepository<SleepTime>, IRepository<SleepTime>
    {
        public SleepTimesRepository(IJarvisTestDbContext context)
            : base(context)
        {
        }
    }
}
