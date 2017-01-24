namespace Jarvis.TestDatabase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Data.Models;

    public interface IJarvisTestDbContext : IDisposable
    {
        IDbSet<SleepTime> SleepTimes { get; set; }

        IDbSet<GetReadyTime> GetReadyTimes { get; set; }

        IDbSet<Joke> Jokes { get; set; }

        int SaveChanges();

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;
    }
}
