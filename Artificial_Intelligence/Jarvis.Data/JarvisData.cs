﻿namespace Jarvis.Data
{
    using System;
    using System.Collections.Generic;
    using Repositories;
    using Repositories.Abstraction;
    using Models;

    public class JarvisData : IJarvisData
    {
        private readonly IJarvisDbContext _context;
        private readonly IDictionary<Type, object> _repositories;

        public JarvisData() 
            : this(new JarvisDbContext())
        {

        }

        public JarvisData(IJarvisDbContext context)
        {
            this._context = context;
            this._repositories = new Dictionary<Type, object>();
        }

        public JokesRepository Jokes
        {
            get
            {
                return (JokesRepository)this.GetRepository<Joke>();
            }
        }

        public SleepTimesRepository SleepTimes
        {
            get
            {
                return (SleepTimesRepository)this.GetRepository<SleepTime>();
            }
        }

        public GetReadyTimesRepository GetReadyTimes
        {
            get
            {
                return (GetReadyTimesRepository)this.GetRepository<GetReadyTime>();
            }
        }

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var repositoryType = typeof(T);

            if (!this._repositories.ContainsKey(repositoryType))
            {
                var type = typeof(GenericRepository<T>);

                if (repositoryType.IsAssignableFrom(typeof(GetReadyTime)))
                {
                    type = typeof(GetReadyTimesRepository);
                }
                else if(repositoryType.IsAssignableFrom(typeof(Joke)))
                {
                    type = typeof(JokesRepository);
                }
                else if (repositoryType.IsAssignableFrom(typeof(SleepTime)))
                {
                    type = typeof(SleepTimesRepository);
                }

                this._repositories.Add(repositoryType, Activator.CreateInstance(type, this._context));
            }

            return (IRepository<T>)this._repositories[repositoryType];
        }
    }
}
