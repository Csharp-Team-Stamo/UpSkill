using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpSkill.Infrastructure.Models.Course
{
    public class CourseDetailsServiceModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CoachFullName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AuthorFullName { get; set; }
        public string AuthorCompany { get; set; }
        public decimal Price { get; set; }
        public string VideoUrl { get; set; }
    }
}
