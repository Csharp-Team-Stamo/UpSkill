namespace UpSkill.Data.Models
{
    using System.Collections.Generic;

    using global::Data.Models;
    using UpSkill.Data.Common.Models;

    public class Employee : BaseDeletableModel<string>
    {
        public string UserId { get; init; }
        public ApplicationUser User { get; init; }

        public ICollection<CoachEmployee> Coaches { get; set; } = new HashSet<CoachEmployee>();

        public ICollection<EmployeeCourse> Students { get; set; } = new HashSet<EmployeeCourse>();

        public ICollection<Grade> Grades { get; set; } = new HashSet<Grade>();

        public ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();
    }
}
