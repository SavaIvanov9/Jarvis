namespace Jarvis.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class GetReadyTime
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string StartTime { get; set; }

        [Required]
        public long DurationInMinutes { get; set; }
    }
}
