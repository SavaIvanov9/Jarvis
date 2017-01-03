namespace Jarvis.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SleepTime
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string StartTime { get; set; }

        [Required]
        public bool IsEnded { get; set; }

        public string Duration { get; set; }
    }
}
