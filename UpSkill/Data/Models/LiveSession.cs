namespace UpSkill.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using UpSkill.Data.Common.Models;

    public class LiveSession : BaseDeletableModel<string>
    {
        public LiveSession()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string CoachId { get; set; }
        public Coach Coach { get; set; }

        public string StudentId { get; set; }
        public Employee Student { get; set; }

        [Required]
        public string Topic { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        public string EventSessionType { get; set; }

        public string JoinSessionUri { get; set; }

        public string CancelationUri { get; set; }

        public string ReschedulingUri { get; set; }

        public int? CoachFeedbackId { get; set; }

        public CoachFeedback CoachFeedback{ get; set; }

        public bool GivenFeedback { get; set; }
    }
}
