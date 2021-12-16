namespace UpSkill.Infrastructure.Models.Course
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CoursesListingCatalogModel
    {
        public string OwnerId { get; set; }

        public ICollection<int> OwnerCourseCollectionIds { get; set; } = new List<int>();

        public List<CourseInListCatalogModel> Courses { get; set; } = new List<CourseInListCatalogModel>();
    }
}
