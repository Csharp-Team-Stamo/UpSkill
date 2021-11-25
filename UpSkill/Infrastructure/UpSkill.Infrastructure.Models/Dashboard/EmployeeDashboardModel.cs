namespace UpSkill.Infrastructure.Models.Dashboard
{
    using System.Collections.Generic;
    using Course;

    public class EmployeeDashboardModel
    {
        public ICollection<CourseInListCatalogModel> Courses { get; set; }
    }
}
