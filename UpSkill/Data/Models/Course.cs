namespace UpSkill.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using UpSkill.Data.Common.Models;
    using static DataConstants.CourseConstants;

    public class Course : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(NameMaxLen)]
        public string Name { get; set; }

        [Required]
        [MaxLength(SessionDescriptionMaxlen)]
        public string Description { get; set; }

        [Required]
        [Range(typeof(decimal), "0.00", "9999.00")]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(SkillsLearnMaxlen)]
        public string SkillsLearn { get; set; }

        [Required]
        [MaxLength(CourseDurationInHoursMaxLen)]
        public string CourseDurationInHours { get; set; }

        [Required]
        [MaxLength(LecturesNumMaxLen)]
        public string LecturesCount { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(AuthorNameMaxLen)]
        public string AuthorFullName { get; set; }

        [Required]
        public string AuthorImageUrl { get; set; }

        [Required]
        [MaxLength(CompanyNameMaxLen)]
        public string CompanyName { get; set; }

        [Required]
        public string CompanyLogoUrl { get; set; }

        [Required]
        public string VideoUrl { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }           

        public ICollection<Lecture> Lectures { get; set; } =
            new HashSet<Lecture>();

        public ICollection<CourseOwner> Owners { get; set; } = 
            new HashSet<CourseOwner>();

        public ICollection<CourseVote> Votes { get; set; } = 
            new HashSet<CourseVote>();

        public ICollection<EmployeeCourse> Students { get; set; } = 
            new HashSet<EmployeeCourse>();
    }
}
