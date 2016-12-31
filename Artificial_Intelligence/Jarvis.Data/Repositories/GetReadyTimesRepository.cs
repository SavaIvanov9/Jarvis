namespace Jarvis.Data.Repositories
{
    using Abstraction;
    using Models;

    public class GetReadyTimesRepository : GenericRepository<GetReadyTime>, IRepository<GetReadyTime>
    {
        public GetReadyTimesRepository(IJarvisDbContext context)
            : base(context)
        {
        }
    }
}
