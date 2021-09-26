namespace UpSkill.Infrastructure.Data.Models
{
    using System.Collections.Generic;

    public class Employee : ApplicationUser
    {
        public ICollection<StudentCourse> StudentCourses { get; set; }

        public ICollection<Grade> Grades { get; set; }

        public ICollection<Invoice<Employee, Company>> CourseInvoices { get; set; }

        public ICollection<Invoice<Employee, Coach>> LiveSessionInvoices { get; set; }
    }
}
