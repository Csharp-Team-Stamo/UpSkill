namespace UpSkill.Data.Models
{
    using System.Collections.Generic;

    using global::Data.Models;
    using UpSkill.Data.Common.Models;

    public class Employee : BaseModel<string>
    {
        public Employee()
        {
            this.StudentCourses = new HashSet<StudentCourse>();
            this.Grades = new HashSet<Grade>();
            this.Invoices = new HashSet<Invoice>();
        }

        public string UserId { get; init; }
        public ApplicationUser User { get; init; }

        public ICollection<CoachEmployee> Coaches { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; }

        public ICollection<Grade> Grades { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
    }
}
