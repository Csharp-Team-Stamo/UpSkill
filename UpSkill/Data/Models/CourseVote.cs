namespace Data.Models
{
    using UpSkill.Data.Common.Models;
    using UpSkill.Data.Models;

    public class CourseVote : BaseDeletableModel<int>
    {
        public int CourseId { get; init; }

        public Course Course { get; init; }

        public string UserId { get; init; }

        public ApplicationUser User { get; init; }

        public int Value { get; init; }
    }
}
