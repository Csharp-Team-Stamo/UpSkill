namespace UpSkill.Infrastructure.Models.Course
{
    using System.Collections.Generic;

    public class CoursesListingCatalogModel
    {
        public string OwnerId { get; set; }

        public ICollection<int> OwnerCourseCollectionIds { get; set; } = new List<int>();

        public ICollection<CourseInListCatalogModel> Courses { get; set; } = new List<CourseInListCatalogModel>();
    }
}
