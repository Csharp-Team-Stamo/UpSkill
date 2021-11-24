namespace UpSkill.Infrastructure.Models.Employee
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UpSkill.Infrastructure.Models.Lecture;

    public class EmployeeCourseDetailsModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string AuthorFullName { get; set; }
        public string AuthorCompanyLogoUrl { get; set; }
        public string AuthorImageUrl { get; set; }
        public string CourseVideoUrl { get; set; }
        public IEnumerable<LectureListingModel> Lectures { get; set; }
    }
}
