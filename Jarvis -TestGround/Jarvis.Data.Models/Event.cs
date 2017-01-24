namespace Jarvis.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [Serializable()]
    public class Event
    {
        [Key]
        public int Id { get; set; }

        public string Date { get; set; }

        public string StartTime { get; set; }

        public string Title { get; set; }
    }
}
