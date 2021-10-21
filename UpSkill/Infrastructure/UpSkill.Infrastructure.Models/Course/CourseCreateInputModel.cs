namespace UpSkill.Infrastructure.Models.Course
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CourseCreateInputModel
    {
        public int CategoryId { get; set; }

        [Required]
        public string CoachId { get; set; }

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
