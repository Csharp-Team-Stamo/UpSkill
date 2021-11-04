namespace UpSkill.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using global::Data.Models;

    using UpSkill.Data.Common.Models;

    public class Course : BaseDeletableModel<int>
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string AuthorFullName { get; set; }

        public string CompanyLogoUrl { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string VideoUrl { get; set; }

        public ICollection<CourseOwner> Owners { get; set; } = new HashSet<CourseOwner>();

        public ICollection<CourseVote> Votes { get; set; } = new HashSet<CourseVote>();

        public ICollection<EmployeeCourse> Students { get; set; } = new HashSet<EmployeeCourse>();
    }
}
