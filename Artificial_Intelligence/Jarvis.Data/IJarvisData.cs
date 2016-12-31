namespace Jarvis.Data
{
    using Repositories;

    public interface IJarvisData
    {
        //IRepository<SleepTime> SleepTimes { get; }

        //IRepository<GetRedyTime> GetRedyTimes { get; }

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
