using System.Collections.Generic;
using UpSkill.Infrastructure.Models.Lecture;

namespace UpSkill.Infrastructure.Models.Course
{
    public class CourseWatchDetailsModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string AuthorFullName { get; set; }
        public string AuthorCompanyLogoUrl { get; set; }
        public string AuthorImageUrl { get; set; }
        public string CourseVideoUrl { get; set; }
        public IEnumerable<LectureListingModel> Lectures { get; set; } = new
            List<LectureListingModel>();
    }
}
