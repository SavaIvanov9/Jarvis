﻿namespace Jarvis.Data
{
    using Repositories;

    public interface IJarvisData
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
