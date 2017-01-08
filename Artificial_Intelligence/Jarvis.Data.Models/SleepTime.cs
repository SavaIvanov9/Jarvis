namespace Jarvis.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [Serializable()]
    public class SleepTime
    {
        [Key]
        public int Id { get; set; }

        public string Date { get; set; }

        public string StartTime { get; set; }

        public bool IsEnded { get; set; }

        public string Duration { get; set; }
    }
}
