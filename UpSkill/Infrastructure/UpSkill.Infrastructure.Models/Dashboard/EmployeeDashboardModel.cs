namespace UpSkill.Infrastructure.Models.Dashboard
{
    using System.Collections.Generic;
    using Course;
    using Coach;

    public class EmployeeDashboardModel
    {
        public ICollection<CourseInListCatalogModel> Courses { get; set; }

        public ICollection<CoachInListCatalogModel> Coaches { get; set; }
    }
}
