namespace UpSkill.Infrastructure.Models.Course
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Category;
    using Coach;

    public class CourseEditInputModel
    {
        public int Id { get; set; }
        public CategoryEditInputModel Category { get; set; }

        public CoachEditInputModel Coach { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string AuthorFullName { get; set; }

        [Required]
        public string AuthorCompany { get; set; }

        [Range(0.00, 1000.00)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public string VideoUrl { get; set; }
    }
}
