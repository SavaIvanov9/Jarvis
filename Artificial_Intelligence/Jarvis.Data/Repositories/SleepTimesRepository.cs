namespace Jarvis.Data.Repositories
{
    using Abstraction;
    using Models;

    public class SleepTimesRepository : GenericRepository<SleepTime>, IRepository<SleepTime>
    {
        public SleepTimesRepository(IJarvisDbContext context)
            : base(context)
        {
        }
    }
}
