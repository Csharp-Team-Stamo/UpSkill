namespace UpSkill.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using UpSkill.Data.Common.Models;

    public class LiveSession : BaseDeletableModel<string>
    {
        public string CoachId { get; set; }
        public Coach Coach { get; set; }

        public string StudentId { get; set; }
        public Employee Student { get; set; }

        public int CategoryId { get; init; }
        public Category Category { get; init; }

        [Required]
        public string Topic { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        public int? CoachFeedbackId { get; set; }

        public CoachFeedback CoachFeedback{ get; set; }

        public bool GivenFeedback { get; set; }
    }
}
