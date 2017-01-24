namespace Jarvis.TestDatabase
{
    using Repositories;

    public interface IJarvisTestData
    {
        EventsRepository Events
        {
            get;
        }

        JokesRepository Jokes
        {
            get;
        }

        SleepTimesRepository SleepTimes
        {
            get;
        }

        GetReadyTimesRepository GetReadyTimes
        {
            get;
        }

        void SaveChanges();
    }
}
