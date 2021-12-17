namespace UpSkill.Data.Models
{
    using Common.Models;

    public class CourseVote : BaseDeletableModel<int>
    {
        public int CourseId { get; init; }
        public Course Course { get; init; }

        public string UserId { get; init; }
        public ApplicationUser User { get; init; }

        public byte Value { get; init; }
    }
}
