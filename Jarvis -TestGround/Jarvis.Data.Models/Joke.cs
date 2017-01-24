namespace Jarvis.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [Serializable()]
    public class Joke
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }
    }
}
