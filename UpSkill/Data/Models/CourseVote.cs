namespace Data.Models
{
    using UpSkill.Data.Common.Models;
    using UpSkill.Data.Models;

    public class CourseVote : BaseModel<int>
    {
        public int CourseId { get; init; }

        public Course Course { get; init; }

        public int Value { get; init; }
    }
}
