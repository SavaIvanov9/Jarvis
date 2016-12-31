namespace Jarvis.Data.Repositories
{
    using Abstraction;
    using Models;

    public class JokesRepository : GenericRepository<Joke>, IRepository<Joke>
    {
        public JokesRepository(IJarvisDbContext context)
            : base(context)
        {
        }
    }
}
