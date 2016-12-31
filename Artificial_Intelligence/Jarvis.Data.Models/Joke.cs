namespace Jarvis.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Joke
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
