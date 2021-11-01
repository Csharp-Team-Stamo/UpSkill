﻿namespace UpSkill.Infrastructure.Models.Course
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using UpSkill.Infrastructure.Models.Category;
    using UpSkill.Infrastructure.Models.Coach;

    public class CourseCreateInputModel
    {
       public int CategoryId { get; set; }

       // public IEnume Category { get; set; }

        //[Required]
        //public string CoachId { get; set; }

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
        [Display(Name = "Author Company Logo URL")]
        public string CompanyLogoUrl { get; set; }

        [Range(0.00, 1000.00)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Video URL")]
        public string VideoUrl { get; set; }
    }
}
