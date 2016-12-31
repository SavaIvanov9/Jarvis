namespace Jarvis.Data.Importer
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Press any key to continue and recreate database. Current data will be lost.");
            Console.ReadKey();

            Console.WriteLine(Environment.NewLine + "Creating new database...");
            var db = new JarvisDbContext();
            Database.SetInitializer(new DropCreateDatabaseAlways<JarvisDbContext>());
            db.Jokes.Count();
            Console.WriteLine("Database created.");
        }
    }
}
