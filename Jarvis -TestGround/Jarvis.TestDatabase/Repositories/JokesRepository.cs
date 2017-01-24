namespace Jarvis.TestDatabase.Repositories
{
    using Abstraction;
    using Data.Models;

    public class JokesRepository : GenericRepository<Joke>, IRepository<Joke>
    {
        public JokesRepository(IJarvisTestDbContext context)
            : base(context)
        {
        }
    }
}
