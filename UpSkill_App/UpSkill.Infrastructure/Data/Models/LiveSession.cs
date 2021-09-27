namespace UpSkill.Infrastructure.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class LiveSession
    {
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string Topic { get; set; }

        public decimal Price { get; set; }

        public DateTime StartOfSession { get; set; }

        public DateTime EndOfSession { get; set; }

        public TimeSpan Duration { get; set; }

        [Required]
        public string CoachId { get; set; }
        public Coach Coach { get; set; }

        [Required]
        public string StudentId { get; set; }
        public Employee Student { get; set; }
    }
}
