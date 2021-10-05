namespace Data.Models
{
    using UpSkill.Data.Common.Models;
    using UpSkill.Data.Models;

    public class CoachVote : BaseDeletableModel<int>
    {
        public string CoachId { get; init; }

        public Coach Coach { get; init; }

        public string UserId { get; init; }

        public ApplicationUser User { get; init; }

        public int Value { get; init; }
    }
}
