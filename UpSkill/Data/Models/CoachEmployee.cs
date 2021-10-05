namespace Data.Models
{
    using UpSkill.Data.Common.Models;
    using UpSkill.Data.Models;

    public class CoachEmployee : BaseDeletableModel<int>
    {
        public string StudentId { get; init; }
        public Employee Student { get; init; }

        public string CoachId { get; init; }
        public Coach Coach { get; init; }
    }
}
