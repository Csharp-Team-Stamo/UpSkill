namespace UpSkill.Infrastructure.Models.Course
{
    using System.ComponentModel.DataAnnotations;

    public class CourseCreateInputModel
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Author Full Name")]
        public string AuthorFullName { get; set; }

        [Required]
        [Display(Name="Author Image URL")]
        public string AuthorImageUrl { get; set; }

        [Required]
        [Display(Name = "Author Company Logo URL")]
        public string CompanyLogoUrl { get; set; }

        [Required]
        [Display(Name = "Author Company Name")]
        public string CompanyName { get; set; }

        [Required]
        public string DurationInHours { get; set; }

        [Required]
        public string LecturesCount { get; set; }

        [Required]
        [MaxLength(150)]
        public string SkillsLearn { get; set; }

        [Range(0.00, 1000.00)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required]
        [Display(Name = "Video URL")]
        public string VideoUrl { get; set; }
    }
}
