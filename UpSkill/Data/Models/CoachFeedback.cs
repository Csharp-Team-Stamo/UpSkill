namespace UpSkill.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Models;
    using static DataConstants.CoachFeedBackConstants;

    public class CoachFeedback : BaseDeletableModel<int>
    {
        public byte Value { get; init; }

        [Required]
        [MaxLength(FeedBackMaxLen)]
        public string Description { get; init; }

        public string LiveSessionId { get; set; }

        public LiveSession liveSession { get; set; }
    }
}
