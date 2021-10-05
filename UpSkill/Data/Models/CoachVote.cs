namespace Data.Models
{
    using UpSkill.Data.Common.Models;
    using UpSkill.Data.Models;

    public class CoachVote : BaseModel<int>
    {
        public string CoachId { get; init; }

        public Coach Coach { get; init; }

        public int Value { get; init; }
    }
}
