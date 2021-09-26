namespace UpSkill.Infrastructure.Data.Models
{
    using System.Collections.Generic;

    public class Employee : ApplicationUser
    {
        public Employee()
        {
            this.StudentCourses = new HashSet<StudentCourse>();
            this.Grades = new HashSet<Grade>();
            this.CourseInvoices = new HashSet<Invoice<Employee, Company>>();
            this.LiveSessionInvoices = new HashSet<Invoice<Employee, Coach>>();
        }

        public ICollection<StudentCourse> StudentCourses { get; set; }

        public ICollection<Grade> Grades { get; set; }

        public ICollection<Invoice<Employee, Company>> CourseInvoices { get; set; }

        public ICollection<Invoice<Employee, Coach>> LiveSessionInvoices { get; set; }
    }
}
