namespace UpSkill.Infrastructure.Models.Course
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Category;
    using Coach;

    public class CourseEditInputModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        //public IEnumerable<AdminCategoryListingServiceModel> AllCategories { get; set; } =
        //    new List<AdminCategoryListingServiceModel>();

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

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
