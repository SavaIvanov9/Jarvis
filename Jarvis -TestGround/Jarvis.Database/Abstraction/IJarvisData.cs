namespace Jarvis.Database.Abstraction
{
    using Jarvis.Database.Abstraction.Repsitories;

    public interface IJarvisData
    {
        EventsRepository Events
        {
            get;
        }
    }
}
