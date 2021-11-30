namespace UpSkill.Infrastructure.Models.Employee
{
    using System.Collections.Generic;

    public class EmployeeAchievementsModel
    {
        public List<CourseInEmployeeAchievementsModel> Courses { get; set; } =
            new List<CourseInEmployeeAchievementsModel>();

        public List<CoachInEmployeeAchievementsModel> Coaches { get; set; } =
            new List<CoachInEmployeeAchievementsModel>();
    }
}
