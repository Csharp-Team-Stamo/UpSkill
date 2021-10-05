namespace UpSkill.Data.Models
{
    using System.Collections.Generic;

    using global::Data.Models;
    using UpSkill.Data.Common.Models;

    public class Owner : BaseDeletableModel<string>
    {
        public Owner()
        {
            this.Employees = new HashSet<Employee>();
        }

        public string UserId { get; init; }
        public ApplicationUser User { get; init; }

        public ICollection<Employee> Employees { get; set; }

        public ICollection<CoachOwner> Coaches { get; set; }

        public ICollection<CourseOwner> Courses { get; set; }
    }
}
