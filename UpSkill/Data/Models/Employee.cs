namespace UpSkill.Data.Models
{
    using System.Collections.Generic;

    using global::Data.Models;
    using UpSkill.Data.Common.Models;

    public class Employee : BaseDeletableModel<string>
    {
        public Employee()
        {
            this.StudentCourses = new HashSet<EmployeeCourse>();
            this.Grades = new HashSet<Grade>();
            this.Invoices = new HashSet<Invoice>();
        }

        public string UserId { get; init; }
        public ApplicationUser User { get; init; }

        public ICollection<CoachEmployee> Coaches { get; set; }

        public ICollection<EmployeeCourse> StudentCourses { get; set; }

        public ICollection<Grade> Grades { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
    }
}
