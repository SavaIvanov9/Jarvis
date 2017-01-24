namespace Jarvis.TestDatabase.Repositories
{
    using Abstraction;
    using Data.Models;

    public class GetReadyTimesRepository : GenericRepository<GetReadyTime>, IRepository<GetReadyTime>
    {
        public GetReadyTimesRepository(IJarvisTestDbContext context)
            : base(context)
        {
        }
    }
}