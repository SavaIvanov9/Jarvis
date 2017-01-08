namespace Jarvis.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [Serializable()]
    public class GetReadyTime
    {
        [Key]
        public int Id { get; set; }

        public string Date { get; set; }

        public string StartTime { get; set; }

        public long DurationInMinutes { get; set; }
    }
}
