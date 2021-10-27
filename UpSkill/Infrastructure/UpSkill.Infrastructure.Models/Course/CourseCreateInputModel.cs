namespace UpSkill.Infrastructure.Models.Course
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using UpSkill.Infrastructure.Models.Category;
    using UpSkill.Infrastructure.Models.Coach;

    public class CourseCreateInputModel
    {
        //public CategoryCreateInputModel Category { get; set; }
        public int CategoryId { get; set; }

        //public CoachCreateInputModel Coach { get; set; }

        [Required]
        public string CoachId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Author Full Name")]
        public string AuthorFullName { get; set; }

        [Required]
        [Display(Name = "Author Company")]
        public string AuthorCompany { get; set; }

        [Range(0.00, 1000.00)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Video URL")]
        public string VideoUrl { get; set; }
    }
}
