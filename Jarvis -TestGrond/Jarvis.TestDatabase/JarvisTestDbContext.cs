namespace Jarvis.TestDatabase
{
    using System.Data.Entity;
    using Jarvis.Data.Models;
    using Jarvis.TestDatabase.Migrations;

    public class JarvisTestDbContext : DbContext, IJarvisTestDbContext
    {
        public JarvisTestDbContext() : base("JarvisTestDb")
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<JarvisTestDbContext, Configuration>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<EmployeeArrivalTrackerDbContext>());
        }

        public virtual IDbSet<Event> Events { get; set; }

        public virtual IDbSet<SleepTime> SleepTimes { get; set; }

        public virtual IDbSet<GetReadyTime> GetReadyTimes { get; set; }

        public virtual IDbSet<Joke> Jokes { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
    }
}