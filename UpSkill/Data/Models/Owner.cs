namespace UpSkill.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using UpSkill.Data.Common.Models;

    public class Owner : BaseDeletableModel<string>
    {
        public Owner()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string UserId { get; init; }
        public ApplicationUser User { get; init; }

        public ICollection<Employee> Employees { get; set; } = 
            new HashSet<Employee>();

        public ICollection<CoachOwner> Coaches { get; set; } = 
            new HashSet<CoachOwner>();

        public ICollection<CourseOwner> Courses { get; set; } = 
            new HashSet<CourseOwner>();
    }
}
