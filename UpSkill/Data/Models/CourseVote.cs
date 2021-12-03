namespace UpSkill.Data.Models
{
    using System.ComponentModel.DataAnnotations;
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
