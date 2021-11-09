namespace UpSkill.Infrastructure.Models.Course
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AdminCourseListingServiceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public string AuthorFullName { get; set; }
        public string AuthorCompanyLogo { get; set; }
        public bool IsDeleted { get; set; }
    }
}
