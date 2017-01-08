using Jarvis.Data.Migrations;

namespace Jarvis.Data
{
    using System.Data.Entity;
    using Models;

    public class JarvisDbContext : DbContext, IJarvisDbContext
    {
        public JarvisDbContext() : base("JarvisDb")
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<JarvisDbContext, Configuration>());
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
