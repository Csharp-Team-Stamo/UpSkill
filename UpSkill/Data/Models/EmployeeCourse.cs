namespace UpSkill.Data.Models
{
    using UpSkill.Data.Common.Models;

    public class EmployeeCourse
    {
        public string StudentId { get; set; }
        public Employee Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
