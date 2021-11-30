namespace UpSkill.Infrastructure.Models.Coach
{
    public class EmployeeCoachSessions
    {
        public string CoachId { get; set; }

        public bool IsCoachSessionPending { get; set; }

        public bool IsFirstCoachSession { get; set; }

        public bool IsFeedbackGiven { get; set; }
    }
}
