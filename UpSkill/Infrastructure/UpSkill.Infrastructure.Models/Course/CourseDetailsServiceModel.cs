using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSkill.Infrastructure.Models.Category;
using UpSkill.Infrastructure.Models.Coach;

namespace UpSkill.Infrastructure.Models.Course
{
    public class CourseDetailsServiceModel
    {
        public int Id { get; set; }

        public CategoryDetailsServiceModel Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string AuthorFullName { get; set; }
        public string AuthorCompany { get; set; }
        public decimal Price { get; set; }
        public string VideoUrl { get; set; }
    }
}
