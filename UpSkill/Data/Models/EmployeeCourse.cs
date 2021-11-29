namespace UpSkill.Data.Models
{
    using System;
    using UpSkill.Data.Common.Models;

    public class EmployeeCourse : BaseDeletableModel<int>
    {
        public EmployeeCourse()
        {
            EnrollDate  = DateTime.UtcNow;
            EndDate = EnrollDate.AddMonths(1);
        }

        public string StudentId { get; set; }
        public Employee Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public DateTime EnrollDate { get; set; }

        public DateTime EndDate { get; set; }

        public byte? Grade { get; set; }
    }
}
