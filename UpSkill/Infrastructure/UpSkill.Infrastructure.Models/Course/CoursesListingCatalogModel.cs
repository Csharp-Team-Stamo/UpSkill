namespace UpSkill.Infrastructure.Models.Course
{
    using System.Collections.Generic;
    using Coach;

    public class CoursesListingCatalogModel
    {
        public string OwnerId { get; set; }

        public ICollection<string> OwnerCoachCollectionIds { get; set; } = new List<string>();

        public ICollection<CourseInListCatalogModel> Coaches { get; set; } = new List<CourseInListCatalogModel>();
    }
}
