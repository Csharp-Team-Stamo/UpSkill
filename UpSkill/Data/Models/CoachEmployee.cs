namespace UpSkill.Data.Models
{
    using Common.Models;

    public class CoachEmployee : BaseDeletableModel<int>
    {
        public string StudentId { get; init; }
        public Employee Student { get; init; }

        public string CoachId { get; init; }
        public Coach Coach { get; init; }
    }
}
