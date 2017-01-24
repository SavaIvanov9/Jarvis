using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Database.Abstraction.Repsitories;
using Jarvis.Database.Abstraction.Repsitories.Base;

namespace Jarvis.Database.Abstraction
{
    public class JarvisData : IJarvisData
    {
        private readonly DbContext _context;
        private readonly IDictionary<Type, object> _repositories;

        public JarvisData()
            : this(new JarvisDbEntities())
        {

        }

        public JarvisData(DbContext context)
        {
            this._context = context;
            this._repositories = new Dictionary<Type, object>();
        }

        public EventsRepository Events
        {
            get
            {
                return (EventsRepository)this.GetRepository<Event>();
            }
        }

        //public JokesRepository Jokes
        //{
        //    get
        //    {
        //        return (JokesRepository)this.GetRepository<Joke>();
        //    }
        //}

        //public SleepTimesRepository SleepTimes
        //{
        //    get
        //    {
        //        return (SleepTimesRepository)this.GetRepository<SleepTime>();
        //    }
        //}

        //public GetReadyTimesRepository GetReadyTimes
        //{
        //    get
        //    {
        //        return (GetReadyTimesRepository)this.GetRepository<GetReadyTime>();
        //    }
        //}

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

                //if (repositoryType.IsAssignableFrom(typeof(GetReadyTime)))
                //{
                //    type = typeof(GetReadyTimesRepository);
                //}
                //else if (repositoryType.IsAssignableFrom(typeof(Joke)))
                //{
                //    type = typeof(JokesRepository);
                //}
                //else if (repositoryType.IsAssignableFrom(typeof(SleepTime)))
                //{
                //    type = typeof(SleepTimesRepository);
                //}
                if (repositoryType.IsAssignableFrom(typeof(Event)))
                {
                    type = typeof(EventsRepository);
                }

                this._repositories.Add(repositoryType, Activator.CreateInstance(type, this._context));
            }

            return (IRepository<T>)this._repositories[repositoryType];
        }
    }
}